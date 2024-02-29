using UnityEngine;
using UnityEngine.Localization.Settings;

namespace WordSorter
{
	public static class Localization
	{
		public static string GetLocalizedMessage(string message)
		{
			var exists = LocalizationSettings.StringDatabase.GetTable("LocalizationTable").GetEntry(message);

			if (exists != null && !string.IsNullOrEmpty(exists.GetLocalizedString()))
				return LocalizationSettings.StringDatabase.GetLocalizedString("LocalizationTable", message);

			return message;
		}
	}
}