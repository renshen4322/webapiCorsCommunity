using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.News.Component
{
    public class NewsIntro
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string thumbnail { get; set; }
        public string news_url { get; set; }
        public string introduction { get; set; }
        public bool is_hot { get; set; }
        public bool offLine { get; set; }
        public long created_at { get; set; }
    }
}
