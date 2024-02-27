using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionNavigation : MonoBehaviour
{
	[SerializeField] private Button buttonLeft, buttonRight;

	private void OnEnable() => LevelList.ChangedWorldNumberEvent += UpdateButtons;

	private void OnDisable() => LevelList.ChangedWorldNumberEvent -= UpdateButtons;

	private void UpdateButtons(int currentWorld, int worldsLength)
	{
		bool left = currentWorld > 0;
		bool right = currentWorld < worldsLength - 1;

		Enable(left, right);
	}

	public void Enable(bool enableLeft, bool enableRight)
	{
		buttonLeft.interactable = enableLeft;
		buttonRight.interactable = enableRight;
	}
}
