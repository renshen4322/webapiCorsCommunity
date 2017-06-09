using AutoMapper;
using Communiry.Entity;
using Community.Common.Exception;
using Community.Contact.News;
using Community.Contact.News.Component;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service
{
    public class NewsService : INewsService
    {
        #region Fields
        private readonly IRepository<NewsEntity> _newsRepository;
        private readonly IRepository<CategoryRelationshipsEntity> _categoryRelationshipsRepository;
        private readonly IDapperRepository _dapperRepository;

        #region sql
        #region 按条件查询新闻

        private const string SELECT_NEWS_COUNT = "select count(1) ";

        private const string SELECT_NEWS_SELECT = "select cn.id as 'Id', cn.title as 'Title',cn.news_url as 'NewsUrl', "
                                                    + "cn.introduction as 'Introduction',cn.thumbnail as 'Thumbnail', "
                                                    + "cn.is_hot as 'IsHot', cn.off_line as 'OffLine',cn.created_date as 'CreatedDate' ";

        private const string SELECT_NEWS_FROM = "{0} from community_news as cn "
                                                + " {1} "
                                                 + "ORDER BY cn.created_date desc {2} ;";//
        private const string SELECT_NEWS_WHERE_CAT = "cn.id in (select object_id from community_category_relationships "
                                                + "where ({0}) "
                                                + "GROUP BY object_id  "
                                                + "HAVING count(object_id)>{1}) ";
        private const string SELECT_NEWS_WHERE_HOT = " is_hot={0} ";
        private const string SELECT_NEWS_WHERE_OFFLINE = " off_line={0} ";


        #endregion
        #endregion
        #endregion
        #region Ctor
        public NewsService(IRepository<NewsEntity> newsRepository,
                           IDapperRepository dapperRepository,
            IRepository<CategoryRelationshipsEntity> categoryRelationshipsRepository)
        {
            this._newsRepository = newsRepository;
            this._dapperRepository = dapperRepository;
            this._categoryRelationshipsRepository = categoryRelationshipsRepository;
        }
        #endregion

        #region Method
        public Task<GetNewsListResponse> GetNewsListAsync(GetNewsList dto)
        {
            return Task.Run(() =>
            {
                string sqlWhere = "";
                switch (dto.hot_type)
                {
                    case Community.Contact.Enum.HotEnum.All:
                        break;
                    case Community.Contact.Enum.HotEnum.Hot:
                        sqlWhere += string.Format(SELECT_NEWS_WHERE_HOT, 1);
                        break;
                    case Community.Contact.Enum.HotEnum.UnHot:
                        sqlWhere += string.Format(SELECT_NEWS_WHERE_HOT, 0);
                        break;
                    default:
                        break;
                }
                switch (dto.off_line)
                {
                    case Community.Contact.Enum.OffLineEnum.All:
                        break;
                    case Community.Contact.Enum.OffLineEnum.OffLine:
                        if (sqlWhere.Length < 1)
                        {
                            sqlWhere += string.Format(SELECT_NEWS_WHERE_OFFLINE, 1);
                        }
                        else
                        {
                            sqlWhere += " and ";
                            sqlWhere += string.Format(SELECT_NEWS_WHERE_OFFLINE, 1);

                        }
                        break;
                    case Community.Contact.Enum.OffLineEnum.UnOffLine:
                        if (sqlWhere.Length < 1)
                        {
                            sqlWhere += string.Format(SELECT_NEWS_WHERE_OFFLINE, 0);
                        }
                        else
                        {
                            sqlWhere += " and ";
                            sqlWhere += string.Format(SELECT_NEWS_WHERE_OFFLINE, 0);

                        }
                        break;
                    default:
                        break;
                }
                if (sqlWhere.Length > 1)
                {
                    sqlWhere = " where " + sqlWhere;
                }
                List<SearchNewsModel> data = new List<SearchNewsModel>();
                int count = 0;
                string sqlData = "";
                string sqlCount = "";
                if (string.IsNullOrEmpty(dto.type))
                {
                    sqlData = string.Format(SELECT_NEWS_FROM, SELECT_NEWS_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    sqlCount = string.Format(SELECT_NEWS_FROM, SELECT_NEWS_COUNT, sqlWhere, "");
                }
                else
                {

                    string[] typeidList = dto.type.Split(',');
                    var builder = new StringBuilder();
                    for (int i = 0; i < typeidList.Length; i++)
                    {
                        builder.Append("category_id= " + typeidList[i]);
                        if (i != typeidList.Length - 1)
                        {
                            builder.Append("||");
                        }
                    }
                    if (sqlWhere.Length > 0)
                    {
                        sqlWhere = sqlWhere + " and " + string.Format(SELECT_NEWS_WHERE_CAT, builder.ToString(), typeidList.Count() - 1);


                    }
                    else
                    {
                        sqlWhere = sqlWhere + " where " + string.Format(SELECT_NEWS_WHERE_CAT, builder.ToString(), typeidList.Count() - 1);
                    }
                    sqlData = string.Format(SELECT_NEWS_FROM, SELECT_NEWS_SELECT, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));

                    sqlCount = string.Format(SELECT_NEWS_FROM, SELECT_NEWS_COUNT, sqlWhere, "");
                }
                data = _dapperRepository.Query<SearchNewsModel>(sqlData).ToList();
                if (data != null && data.Count() > 0)
                {
                    count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                }
                GetNewsListResponse resp = new GetNewsListResponse();
                resp.data = Mapper.Map<List<NewsIntro>>(data);
                resp.total = count;
                return resp;
            });
        }

        public Task<CreateNewsResponse> CreateNewsAsync(CreateNews dto)
        {
            return Task.Run(() =>
            {
                var id = Guid.NewGuid();
                var dateTime = DateTime.Now;
                NewsEntity entity = new NewsEntity()
                {
                    CreatedDate = dateTime,
                    Id = id,
                    Introduction = dto.introduction,
                    IsHot = dto.is_hot,
                    NewsUrl = dto.news_url,
                    OffLine = false,
                    Thumbnail = dto.thumbnail_url,
                    Title = dto.title
                };
                _newsRepository.Insert(entity);
                _categoryRelationshipsRepository.Insert(new CategoryRelationshipsEntity() { 
                 CategoryId=dto.type_id,
                 CreatedDate = dateTime,
                 ObjectId=id
                });
                return new CreateNewsResponse() { news_id = id };
            });
        }
        #endregion




        public Task<Community.Contact.News.UpdateNewsOffLineStatusResponse> UpdateNewsOffLineStatusAsync(Contact.News.UpdateNewsOffLineStatus dto)
        {
            return Task.Run(() =>
            {
                var newsEntity = _newsRepository.Table.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (newsEntity == null) throw new RequestErrorException("该新闻不存在!");

                newsEntity.OffLine = dto.off_line;
                _newsRepository.Update(newsEntity);
                return new Community.Contact.News.UpdateNewsOffLineStatusResponse();

            });
        }

        public Task<Contact.News.UpdateNewsHotStatusResponse> UpdateNewsHotStatusAsync(Contact.News.UpdateNewsHotStatus dto)
        {
            return Task.Run(() =>
            {
                var newsEntity = _newsRepository.Table.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (newsEntity == null) throw new RequestErrorException("该新闻不存在!");

                newsEntity.IsHot = dto.is_hot;
                _newsRepository.Update(newsEntity);
                return new Community.Contact.News.UpdateNewsHotStatusResponse();

            });
        }

        public Task<UpdateNewsResponse> UpdateNewsAsync(UpdateNews dto)
        {
            return Task.Run(() =>
            {
                var newsEntity = _newsRepository.Table.Where(t => t.Id.Equals(dto.id)).SingleOrDefault();
                if (newsEntity == null) throw new RequestErrorException("该新闻不存在!");

                newsEntity.Introduction = dto.introduction;
                newsEntity.IsHot = dto.is_hot;
                newsEntity.NewsUrl = dto.news_url;
                newsEntity.Thumbnail = dto.thumbnail_url;
                newsEntity.Title = dto.title;
                newsEntity.OffLine = dto.off_line;
                _newsRepository.Update(newsEntity);
                return new Community.Contact.News.UpdateNewsResponse();

            });
        }
    }
}

