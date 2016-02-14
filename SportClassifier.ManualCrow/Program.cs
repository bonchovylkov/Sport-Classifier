using SportClassifier.Data;
using SportClassifier.Models;
using SportClassifier.Web.Infrastructure.Helpers;
using SportClassifier.Web.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClassifier.ManualCrow
{
    class Program
    {
        private static UowData DbContext;
        private static CrowlingService crowler;
        private static ClasifyService classifier;
        static void Main(string[] args)
        {
            DbContext = new UowData();
          //  crowler = new CrowlingService(DbContext);
            classifier = new ClasifyService(DbContext);
             classifier.TrainModel();
            //var news = DbContext.NewsItems.All().ToList();
            //for (int i = 0; i < news.Count; i++)
            //{
            //    var item = news[i];
            //    item.CleanContent = BaseHelper.ScrubHtml(item.Content);
            //    DbContext.NewsItems.Update(item);
            //    Console.WriteLine(i);
            //}

            // DbContext.SaveChanges();

            
           // UpdateCategories();
            //try
            //{
            //    crowler.Crow();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);

            //}

        }

      
        private static void UpdateCategories()
        {
            List<Category> allCat = DbContext.Categories.All(new string[] { "ParentCategory" }).ToList();

            for (int i = 0; i < allCat.Count; i++)
            {

                int? baseId = null;
                Category parent = allCat[i].ParentCategory;
                int lastId = allCat[i].Id;
                while (true)
                {

                    if (parent == null)
                    {

                        baseId = lastId;
                        break;
                    }
                    else
                    {
                        lastId = parent.Id;
                        parent = DbContext.Categories.All(new string[] { "ParentCategory" }).FirstOrDefault(s => s.Id == lastId).ParentCategory;
                    }

                }

                allCat[i].BaseCategoryId = baseId;
                DbContext.Categories.Update(allCat[i]);
            }

            DbContext.SaveChanges();
        }
    }
}
