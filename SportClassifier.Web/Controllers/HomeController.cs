using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportClassifier.Web.Infrastructure.Paging;
using SportClassifier.Web.Infrastructure.Services.Contracts;
using SportClassifier.Web.Infrastructure.Const;
using SportClassifier.Web.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace SportClassifier.Web.Controllers
{
    public class HomeController : BaseController
    {
        private INewsService newsService;
        private ICrowlingService crowlService;
        private IClasifyService classifyService;

        public HomeController(INewsService newsService,ICrowlingService crowlService,IClasifyService classifyService)
        {
            this.newsService = newsService;
            this.crowlService = crowlService;
            this.classifyService = classifyService;
        }

        public ActionResult Index(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var news = newsService.GetLaterstNews(currentPageIndex, Constants.DEFAULT_PAGE_SIZE);
            return View(news);
        }

        public ActionResult TrainingData(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var news = newsService.GetTrainingData(currentPageIndex, Constants.DEFAULT_PAGE_SIZE);
            return View(news);
        }

        public ActionResult TestData(int? page)
        {
            ViewData["ShowClassifyBtn"] = true;
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            var news = newsService.GetTestData(currentPageIndex, Constants.DEFAULT_PAGE_SIZE);
            return View(news);
        }

        public ActionResult ClassifyArticle(int articleId)
        {
            string category = classifyService.ClassifyArticle(articleId);
                return Json(category, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult CrowOptions()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CrowTrainData()
        {
           int count = crowlService.Crow(true);
           return Json(count, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNews(int id)
        {

            NewsItemViewModel article = newsService.GetArticleById(id);
            return View(article);
        }



        public ActionResult ListAllTestResults()
        {
            return View();
        }

        public ActionResult ReadAll([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult news = this.newsService.GetCategorizedNews(request);
            classifyService.ClassifyDataSourceResult(news);
            return Json(news, JsonRequestBehavior.AllowGet);
        }   
    }
}