

using System;

namespace SportClassifier.Web.Infrastructure.Classifier
{
	/// <summary>
	/// Defines an interface for making a classifier trainable.
	/// </summary>
	public interface ITrainable
	{
		void TeachMatch(string input);
		void TeachMatch(string category, string input,int? catId=null);
		void TeachNonMatch(string input);
		void TeachNonMatch(string category, string input,int? catId=null);
	}
}
