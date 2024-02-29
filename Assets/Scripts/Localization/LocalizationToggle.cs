using UnityEngine;
using UnityEngine.Localization.Settings;

namespace WordSorter
{
	public class LocalizationToggle : MonoBehaviour
	{
		private int currentLocale = 0;

		private void Start() => SetCurrentLocale();

		private void SetCurrentLocale()
		{
			for (int a = 0; a < LocalizationSettings.AvailableLocales.Locales.Count; a++)
			{
				string identifier = LocalizationSettings.AvailableLocales.Locales[a].Identifier.ToString();
				string currentIdentifier = LocalizationSettings.SelectedLocale.Identifier.ToString();
				if (identifier.Equals(currentIdentifier))
				{
					currentLocale = a;
					return;
				}
			}
		}

		public void Toggle()
		{
			currentLocale++;

			if (currentLocale >= LocalizationSettings.AvailableLocales.Locales.Count)
				currentLocale = 0;

			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocale];
		}
	}
}