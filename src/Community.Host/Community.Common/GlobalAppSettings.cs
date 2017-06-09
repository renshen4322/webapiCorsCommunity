using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common
{
  public static  class GlobalAppSettings
    {
      public static string OauthServiceUrl = ConfigurationManager.AppSettings["OAuthServiceURL"];

      public static string SwaggerUrl = ConfigurationManager.AppSettings["swaggerUrl"];
      public static string DataProvider = ConfigurationManager.AppSettings["DataProvider"];
      public static string MySqlConnection = ConfigurationManager.AppSettings["MysqlConnection"];

      public static string QiniuAccessKey = ConfigurationManager.AppSettings["QinIuAccessKey"];
      public static string QiniuSecretKey = ConfigurationManager.AppSettings["QinIuSecretKey"];
      public static string QiniuScope = ConfigurationManager.AppSettings["QinIuScope"];
      public static int QiniuExpires =Convert.ToInt32(ConfigurationManager.AppSettings["QinIuExpires"]);
    }
}
