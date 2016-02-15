using SportClassifier.Data;
using SportClassifier.Web.Infrastructure.Paging;
using SportClassifier.Web.Infrastructure.Services.Contracts;
using SportClassifier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace SportClassifier.Web.Infrastructure.Services
{
    public class NewsService : BaseService,INewsService
    {
        public NewsService(IUowData data) : base(data)
        {

        }

        public IPagedList<NewsItemViewModel> GetLaterstNews(int currentPageIndex, int defaultPageSize)
        {
            //change this with normal paging - this one get all results
            //it's just for the beggining
            return this.Data.NewsItems.All().Project().To<NewsItemViewModel>().OrderByDescending(s=>s.DatePublished).ToPagedList(currentPageIndex, defaultPageSize);
                
        }


        public NewsItemViewModel GetArticleById(int id)
        {
            var item = this.Data.NewsItems.FirstOrDefault(s => s.Id == id);
            var result = Mapper.Map<NewsItemViewModel>(item);
            return result;
        }


        public IPagedList<NewsItemViewModel> GetTrainingData(int currentPageIndex, int defaultPageSize)
        {
            return this.Data.NewsItems.All()
                .Where(s=>s.IsForTest !=true)
                .Project()
                .To<NewsItemViewModel>()
                .OrderByDescending(s=>s.DatePublished)
                .ToPagedList(currentPageIndex, defaultPageSize);
        }

        public IPagedList<NewsItemViewModel> GetTestData(int currentPageIndex, int defaultPageSize)
        {
            return this.Data.NewsItems.All()
                .Where(s=>s.IsForTest ==true)
                .Project()
                .To<NewsItemViewModel>()
                .OrderByDescending(s=>s.DatePublished)
                .ToPagedList(currentPageIndex, defaultPageSize);
        }


        public Kendo.Mvc.UI.DataSourceResult GetCategorizedNews(DataSourceRequest request)
        {
            return this.Data.NewsItems.All()
                .Where(s=>s.IsForTest !=true)
                .Project()
                .To<NewsItemViewModel>()
                .OrderByDescending(s=>s.DatePublished)
                .ToDataSourceResult(request);
        }
    }
}