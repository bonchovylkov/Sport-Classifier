using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Models
{
    public class NewsItemViewModel : IMapFrom<NewsItem>, IHaveCustomMappings
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string MainPic { get; set; }
        public long DatePublished { get; set; }

        //public List<string> Images { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

        public string Href { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Media { get; set; }
        public string Content { get; set; }
        public string CleanContent { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public bool UsedForClassication { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
             configuration.CreateMap<NewsItem, NewsItemViewModel>()
             .ForMember(s => s.Categories, p => p.MapFrom(d => d.Categories))
             
             .ReverseMap();
        }
    }
}