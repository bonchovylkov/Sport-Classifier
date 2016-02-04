using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportClassifier.Data;
using AutoMapper.QueryableExtensions;
using Agromo.Web.Infrastructure.Services;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class HomeService :BaseService, IHomeService
    {
        public HomeService(IUowData data) : base(data)
        {
        }

        //public List<ArticleViewModel> GetLatestArticles(int count)
        //{
        //    return this.Data.Articles.All().OrderBy(s => s.Created).Take(count).Project().To<ArticleViewModel>().ToList();
        //}
    }
}