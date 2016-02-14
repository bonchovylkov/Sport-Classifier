using SportClassifier.Web.Infrastructure.Classifier.Bayesian;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportClassifier.Web.Infrastructure.Services.Contracts
{
    public interface IClasifyService : ICategorizedWordsDataSource
    {
        int TrainModel();

        string ClassifyArticle(int articleId);
    }
}