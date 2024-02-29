using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class WorldTitle : MonoBehaviour
	{
		private TextMeshProUGUI text;

		private void OnEnable() => LevelList.SelectedWorldTitleEvent += UpdateWorldTitle;

		private void OnDisable() => LevelList.SelectedWorldTitleEvent -= UpdateWorldTitle;

		private void UpdateWorldTitle(string worldName)
		{
			if (text == null)
				text = GetComponentInChildren<TextMeshProUGUI>();

			text.text = worldName;
		}
	}
}