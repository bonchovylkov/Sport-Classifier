using AutoMapper;
using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Models
{
    public class CategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? BaseCategoryId { get; set; }
        public string BaseCategory { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Category, CategoryViewModel>()
                .ForMember(m => m.BaseCategory, opt => opt.MapFrom(t => t.BaseCategory != null ? t.BaseCategory.Name : null))

             .ReverseMap();
        }
    }
}