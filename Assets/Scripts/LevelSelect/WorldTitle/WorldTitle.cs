using TMPro;
using UnityEngine;

public class WorldTitle : MonoBehaviour
{
	private TextMeshProUGUI text;

	void Start()
	{
		text = GetComponentInChildren<TextMeshProUGUI>();
	}

	private void OnEnable() => LevelList.SelectedWorldTitleEvent += UpdateWorldTitle;

	private void OnDisable() => LevelList.SelectedWorldTitleEvent -= UpdateWorldTitle;

	private void UpdateWorldTitle(string worldName)
	{
		text.text = worldName;
	}
}
