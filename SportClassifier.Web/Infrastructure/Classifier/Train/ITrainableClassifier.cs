

using System;

namespace SportClassifier.Web.Infrastructure.Classifier
{
	public interface ITrainableClassifier : ICategorizedClassifier, ITrainable
	{
	}
}
