using UnityEngine;
using UnityEngine.Localization.Settings;

namespace WordSorter
{
	public static class Localization
	{
		private const string TableReference = "LocalizationTable";

		public static string GetLocalizedMessage(string message, string tableReference = TableReference)
		{
			var database = LocalizationSettings.StringDatabase;
			var existEntry = database.GetTable(tableReference).GetEntry(message);

			if (existEntry != null && !string.IsNullOrEmpty(existEntry.GetLocalizedString()))
				return database.GetLocalizedString(tableReference, message);

			return message;
		}
	}
}