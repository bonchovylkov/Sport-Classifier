using SportClassifier.Web.Infrastructure.Paging;
using SportClassifier.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.Web.Infrastructure.Services.Contracts
{
    public interface INewsService
    {
        IPagedList<NewsItemViewModel> GetLaterstNews(int currentPageIndex, int defaultPageSize);

        NewsItemViewModel GetArticleById(int id);

        IPagedList<NewsItemViewModel> GetTrainingData(int currentPageIndex, int defaultPageSize);

        IPagedList<NewsItemViewModel> GetTestData(int currentPageIndex, int defaultPageSize);
    }
}
