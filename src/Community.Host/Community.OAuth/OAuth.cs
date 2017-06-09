using Communiry.Entity;
using Community.Common;
using Community.Core.Data;
using Community.EntityFramework;
using Community.IService;
using Community.OAuth.Exception;
using Community.Service;
using Microsoft.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Community.OAuth
{
    public class OAuth : IOAuth
    {
        const string BEARER = "Bearer";
        const string MISSING_AUTH = "Bearer realm=\"Api\",error=\"invalid_token\", error_description=\"The access token is missing\"";
        const string BAD_TOKEN = "Bearer realm=\"Api\", error=\"invalid_token\", error_description=\"The access token is invalid\"";
        const string EXPIRED_TOKEN = "Bearer realm=\"Api\", error=\"invalid_token\", error_description=\"The access token is expired\"";
        const string Authorization = "Authorization";
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public OAuthToken AcquireToken(string authorization)
        {

            var tokenString = ExtractTokenString(authorization);

            OAuthToken cachedOAuthToken = null;
            if (string.IsNullOrEmpty(tokenString))
            {
                // _logger.Debug("Ending request with code {0}, message {1}...", 401, MISSING_AUTH);
                cachedOAuthToken = new OAuthToken();
                cachedOAuthToken.Error = new Error() { Msg = MISSING_AUTH };
            }
            else
            {
                cachedOAuthToken = CacheHelper.Get<OAuthToken>(tokenString);


                if (cachedOAuthToken == null)
                {
                    var result = Validate(tokenString);
                    if (string.IsNullOrEmpty(result))
                    {
                        var errOT=new OAuthToken();
                        errOT.Error= new Error() { Msg = BAD_TOKEN };
                        return errOT;
                      //  throw new OAuthTokenValidationException();
                    }
                    var oauthUser = JsonDeserialize<OauthUser>(result);
                    cachedOAuthToken = new OAuthToken();
                    cachedOAuthToken.OauthUserId = oauthUser.userId;
                    cachedOAuthToken.email = oauthUser.email;
                    cachedOAuthToken.mobile = oauthUser.mobile;
                    cachedOAuthToken.userName = oauthUser.userName;
                    cachedOAuthToken.Token = tokenString;
                    cachedOAuthToken.ExpirationDateUTC = DateTime.UtcNow.AddMinutes(30).ToString();
                    cachedOAuthToken.UserId = GetUserId(oauthUser.userId);
                 
                    #region error code
                    //if (oAuthToken.ResponseCode == "0")
                   // {
                        // _logger.Debug("Validation success, added token object to cache");
                       
                   // }
                    //else if (oAuthToken.ResponseCode == "100013")
                    //{
                    //   // _logger.Debug("Ending request with code {0}, message {1}...", 401, BAD_TOKEN);
                    //    res.TerminateWith(401, BAD_TOKEN);
                    //}
                    //else if (oAuthToken.ResponseCode == "100010")
                    //{
                    //    _logger.Debug("Ending request with code {0}, message {1}...", 401, EXPIRED_TOKEN);
                    //    res.TerminateWith(401, EXPIRED_TOKEN);
                    //}
                    #endregion
                    #region 获取系统用户信息
                        
                    #endregion
                    CacheHelper.Insert<OAuthToken>(tokenString, cachedOAuthToken, 30);
                   
                }
                else
                {
                    //  _logger.Debug("Hit cached token");
                    var expireUtcDate = DateTime.Parse(cachedOAuthToken.ExpirationDateUTC);
                    if (expireUtcDate <= DateTime.UtcNow.AddSeconds(15))
                    {
                        // _logger.Debug("Cached Token expired, ending request with code {0}, message {1}...", 401, EXPIRED_TOKEN);
                        CacheHelper.Remove(tokenString);
                        cachedOAuthToken.Error = new Error() { Msg = EXPIRED_TOKEN };
                    }
                    else {
                        if (cachedOAuthToken.UserId == null)
                        {
                            cachedOAuthToken.UserId = GetUserId(cachedOAuthToken.OauthUserId);
                        }
                    }
                  
                }

                if (cachedOAuthToken == null)
                {
                    throw new OAuthTokenValidationException();
                }
            }
            // return cachedOAuthToken;
            return cachedOAuthToken;
        }
        /// <summary>
        /// 获取用户对应id
        /// </summary>
        /// <param name="baseUserId"></param>
        /// <returns></returns>
        Guid? GetUserId(int baseUserId) {
            try
            {
                IDapperRepository dbRepository = new MysqlDapperRepository(GlobalAppSettings.MySqlConnection);
                return dbRepository.QuerySingleOrDefault<Guid?>("select id from community_base_user where user_base_id=" + baseUserId);
    
            }
            catch (System.Exception ex)
            {
                _logger.Error("tt="+baseUserId);  
                _logger.Error("gtd:{0}, failed because {1}", 1, "x");  
                _logger.Error("get userid by OauthId:{0}, failed because {1}", baseUserId, ex.Message);             
                return null;
            }
              }
        string Validate(string tokenString)
        {
          
            #region 正式逻辑
            var url = GlobalAppSettings.OauthServiceUrl;
            // _logger.Debug("Validating access token string {0} against {1}", tokenString, url);
            try
            {
                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = "GET";
                req.Headers.Add("Authorization", tokenString);
               // req.ContentType = "application/x-www-form-urlencoded";
                //using (var rs = req.GetRequestStream())
                //{
                //    var formDataString = "access_token=" + tokenString;
                //    var formData = Encoding.UTF8.GetBytes(formDataString);
                //    rs.Write(formData, 0, formData.Length);
                //}

                var res = req.GetResponse();
                var result = string.Empty;
                using (var receivedStream = res.GetResponseStream())
                {
                    var streamReader = new StreamReader(receivedStream);
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (WebException ex)
            {
                return "";
                _logger.Error("Validation on access token string {0} against {1}, failed because {2}", tokenString, url, ex.Message);
                throw new OAuthTokenValidationException(ex.Message, ex.InnerException);
            }
            #endregion
        }

        string ExtractTokenString(string authorization)
        {
            var tokenString = string.Empty;
            var authz = authorization;
            if (!string.IsNullOrEmpty(authorization) && authz.Contains(BEARER))
            {
               // _logger.Debug("Reading access token from Header base on Bearer scheme...");
                tokenString = authz.TrimStart(BEARER.ToCharArray()).Trim();
            }
            else
            {
               //_logger.Debug("Reading access token from request parameter...");
                tokenString = authorization;
            }
            return tokenString;
        }

        T JsonDeserialize<T>(string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return default(T);
            }

            var ser = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(txt)))
            {
                return (T)ser.ReadObject(ms);
            }
        }
    }
}
