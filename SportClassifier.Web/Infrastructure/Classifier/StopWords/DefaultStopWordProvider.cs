

using System;

namespace SportClassifier.Web.Infrastructure.Classifier
{
	public class DefaultStopWordProvider : IStopWordProvider
	{
		#region Fields
		string[] _stopWords = 
		{
			"a", "and", "the", "me", "i", "of", "if", "it",  
			"is", "they", "there", "but", "or", "to", "this", "you", 
			"in", "your", "on", "for", "as", "are", "that", "with",
			"have", "be", "at", "or", "was", "so", "out", "not", "an"
		};

		string[] _sortedStopWords = null;
		#endregion

		public string[] StopWords { get { return _stopWords; } }

		public DefaultStopWordProvider()
		{
			_sortedStopWords = _stopWords;
			Array.Sort(_sortedStopWords);
		}

		public bool IsStopWord(string word)
		{
			if (word == null || word == string.Empty)
				return false;
			else
				return Array.BinarySearch(_sortedStopWords, word.ToLower()) >= 0;
		}
	}
}