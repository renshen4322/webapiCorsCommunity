using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Const
{
    public class ImageServiceConst
    {
        /// <summary>
        /// 查询图片列表
        /// </summary>
        public const string SELECT_SYSTEM_IMG_SQL = "select csi.id as 'id',csi.description as 'description',cspn.`name` as 'project_name' ,csi.image_name as  'image_name',csi.image_uri as 'image_uri' ,csi.created_date as 'created_date' "
                                                    + " from community_sys_image as csi "
                                                    + " INNER JOIN "
                                                    + " community_sys_project_name as cspn "
                                                    + " on csi.project_id=cspn.id {0} "
                                                    + " order by csi.created_date DESC "
                                                    + " LIMIT @start,@length;";

        public const string SELECT_SYSTEM_IMG_WHERE_SQL = " and csi.project_id=@projectId ";
        public const string SELECT_SYSTEM_IMG_COUNT_SQL = "select COUNT(1) "
                                                    + " from community_sys_image as csi "
                                                    + " INNER JOIN "
                                                    + " community_sys_project_name as cspn "
                                                    + " on csi.project_id=cspn.id {0} ;";
    }
}
