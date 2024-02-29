using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace WordSorter
{
	public class WorldTitle : MonoBehaviour
	{
		private TextMeshProUGUI text;
		private string currentWorldName = "";

		private void OnEnable()
		{
			LevelList.SelectedWorldTitleEvent += UpdateWorldTitle;
			LocalizationSettings.SelectedLocaleChanged += RefreshWorldTitleLocalization;
		}

		private void OnDisable()
		{
			LevelList.SelectedWorldTitleEvent -= UpdateWorldTitle;
			LocalizationSettings.SelectedLocaleChanged -= RefreshWorldTitleLocalization;
		}

		private void RefreshWorldTitleLocalization(Locale locale)
		{
			UpdateWorldTitle(currentWorldName);
		}

		private void UpdateWorldTitle(string worldName)
		{
			currentWorldName = worldName;

			if (text == null)
				text = GetComponentInChildren<TextMeshProUGUI>();

			text.text = Localization.GetLocalizedMessage(worldName);
		}
	}
}