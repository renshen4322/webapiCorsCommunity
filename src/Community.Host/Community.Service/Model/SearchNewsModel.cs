using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model
{
  public  class SearchNewsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Thumbnail { get; set; }
        public string Introduction { get; set; }
        public string NewsUrl { get; set; }
        public bool IsHot { get; set; }
        public bool OffLine { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
