using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Const
{
    public class CommentServiceConst
    {
        #region Sql
        /// <summary>
        /// 通过作品id查询作品评论
        /// </summary>
        public const string SELECT_WORKS_COMMENT_LIST = "select cw.`name` as 'Title' ,cw.id as 'Id',cbu.nick_name as 'Author',cc.id as 'CommentId',cc.content as 'Content',cc.gmt_create as 'CreatedAt' "
                                                        + "from community_comment as cc  "
                                                        + "inner JOIN community_works as cw "
                                                        + "on cc.target_id=cw.id and cc.is_offline=0 {0} "
                                                        + "INNER JOIN community_base_user as cbu "
                                                        + "on cc.author_id=cbu.id "
                                                        +" {1} "
                                                        + "ORDER BY CreatedAt desc "
                                                        +"limit @start,@length ;";
        public const string SELECT_WORKS_COMMENT_WHERE_1 = " where cw.`name` like @title ";
        public const string SELECT_WORKS_COMMENT_WHERE_0 = " and cw.id=@worksId ";
       
        /// <summary>
        /// 查询所有产品评论
        /// </summary>
        public const string SELECT_PRODUCT_COMMENT_LIST = "select cp.`name` as 'Title' ,cp.id as 'Id',cbu.nick_name as 'Author',cc.id as 'CommentId',cc.content as 'Content',cc.gmt_create as 'CreatedAt' "
                                                      + "from community_comment as cc  "
                                                      + "inner JOIN community_product as cp "
                                                      + "on cc.target_id=cp.id and cc.is_offline=0 {0} "
                                                      + "INNER JOIN community_base_user as cbu "
                                                      + "on cc.author_id=cbu.id "
                                                       + " {1} "
                                                      + "ORDER BY CreatedAt desc "
                                                      + "limit @start,@length";
        public const string SELECT_PRODUCT_COMMENT_WHERE_1 = " where cp.`name` like @title ";
        public const string SELECT_PRODUCT_COMMENT_WHERE_0 = " and cp.id=@worksId ";
       
        /// <summary>
        /// 查询作品评论数量
        /// </summary>
        public const string QUERY_WORKS_COMMENT_COUNT ="select COUNT(1) from community_comment as cc "
                                                        + "INNER JOIN community_works as cw "
                                                        + "on cc.target_id=cw.id  and cc.is_offline=0 {0} "
                                                        + " {1} "
                                                        + ";";
        /// <summary>
        /// 查询产品评论数量
        /// </summary>
        public const string QUERY_PRODUCT_COMMENT_COUNT = "select COUNT(1) from community_comment as cc "
                                                        + "INNER JOIN community_product as cp "
                                                        + "on cc.target_id=cp.id  and cc.is_offline=0 {0} "
                                                        + " {1} "
                                                        + ";";
        /// <summary>
        /// 查询所以数量
        /// </summary>
        public const string QUERY_ALL_COMMENT_COUNT = "select COUNT(1) from community_comment where  cc.is_offline=0 ";

        public const string QUERY_COMMENT_COUNT_WHERE = " where target_id=@targetId; ";


        public const string QUERY_REPORT_LIST = "select * from community_comment_report "
                                                +"where audit_status='pending' "
                                                +"ORDER BY gmt_create DESC "
                                                +"limit @start,@length;"; 

        #endregion
    }
}
