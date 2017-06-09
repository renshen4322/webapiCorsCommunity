﻿using Communiry.Entity.EnumType;
using Community.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity
{
    public class ProductEntity : BaseEntity
    {
       public Guid Id { get; set; }
       public ProductImportTypeEnum ImportType
       {
           get
           {
               return (ProductImportTypeEnum)Enum.Parse(typeof(ProductImportTypeEnum), DbImportType, true);
           }
           set
           {
               this.DbImportType = value.ToString();
           }
       }
       public string DbImportType { get; set; }
       public int? OriginId { get; set; }
       public string Name { get; set; }
       public Guid UserId { get; set; }
       public string Thumbnail { get; set; }
       public string Images { get; set; }
        public string ImageThumbnail { get; set; }
        public float? Cost { get; set; }
        public string Introduction { get; set; }
        public bool OffLine { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
