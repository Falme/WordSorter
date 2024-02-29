using System;

namespace WordSorter
{
	[Serializable]
	public class WordData
	{
		private const string TableReference = "LocalizationWords";

		public string word;
		public string scrambledWord;

		public string TranslatedWord => Localization.GetLocalizedMessage(word, TableReference);
		public string TranslatedScrambledWord => Localization.GetLocalizedMessage(scrambledWord, TableReference);
	}
}