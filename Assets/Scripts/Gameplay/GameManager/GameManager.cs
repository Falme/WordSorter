using System;
using UnityEngine;

namespace WordSorter
{
	public class GameManager : MonoBehaviour
	{
		private const string BodyText = "Congratulations! \n To the next Level!";
		private const string UndefinedLevelErrorMessage =
			"LevelConfiguration was not set, please check the Level Selection and Level Scriptable Objects";

		[Header("Scene References")]
		[SerializeField] private ShelfManager shelfManager;
		[SerializeField] private PanelWords panelWords;

		private LevelConfiguration levelConfiguration;

		private void Start()
		{
			levelConfiguration = LevelManager.Instance.CurrentLevel;

			if (levelConfiguration == null)
			{
				Debug.LogError(UndefinedLevelErrorMessage);
				return;
			}

			shelfManager.Initialize(levelConfiguration);
			panelWords.Initialize(levelConfiguration);
		}

		private void OnEnable() => ShelfManager.CompareWordsEvent += CompareWords;
		private void OnDisable() => ShelfManager.CompareWordsEvent -= CompareWords;

		private void CompareWords(string[] words)
		{
			if (AreAllWordsMatching(words, levelConfiguration.shelvesData))
			{
				if (LevelManager.Instance.CurrentLevel.nextLevel != null)
					ShowNextLevelPopup();
				else
					ShowToLevelSelectPopup();
			}
		}

		private void ShowToLevelSelectPopup()
		{
			Popup.Instance.OpenPopup(
					BodyText,
					PopupType.OK,
					() =>
					{
						LevelManager.Instance.LoadScene("LevelSelect");
					}
				);
		}

		private void ShowNextLevelPopup()
		{
			Popup.Instance.OpenPopup(
					BodyText,
					PopupType.OK,
					() =>
					{
						LevelManager.Instance.CurrentLevel = LevelManager.Instance.CurrentLevel.nextLevel;
						LevelManager.Instance.LoadScene("Gameplay");
					}
				);
		}

		private bool AreAllWordsMatching(string[] shelfWords, WordData[] wordDatas)
		{
			foreach (WordData wordData in wordDatas)
			{
				bool foundMatch = false;
				string correctWord = wordData.word;

				foreach (string shelfWord in shelfWords)
				{
					if (correctWord.Equals(shelfWord))
						foundMatch = true;
				}

				if (!foundMatch) return false;
			}

			return true;
		}

		public void Restart()
		{
			shelfManager.Restart();
			panelWords.Restart();
		}
	}
}