using UnityEngine;

namespace WordSorter
{
	public class GameManager : MonoBehaviour
	{
		private const string NextLevelBodyText = "Congratulations! \n To the next Level!";
		private const string LevelSelectBodyText = "Completed The Game! \n To the Level Selection!";
		private const string UndefinedLevelErrorMessage =
			"LevelConfiguration was not set, please check the Level Selection and Level Scriptable Objects";

		[Header("Scene References")]
		[SerializeField] private ShelfManager shelfManager;
		[SerializeField] private PanelWords panelWords;
		[SerializeField] private Tutorial tutorial;

		private Level level;

		private void Start()
		{
			level = LevelManager.Instance.CurrentLevel;

			if (IsLevelUndefined(level)) return;

			shelfManager.Initialize(level);
			panelWords.Initialize(level);
			tutorial.Initialize(level);
		}

		private void OnEnable() => ShelfManager.CompareWordsEvent += CompareWords;
		private void OnDisable() => ShelfManager.CompareWordsEvent -= CompareWords;

		private void CompareWords(string[] words)
		{
			if (AreAllWordsMatching(words, level.shelvesData))
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
					LevelSelectBodyText,
					PopupType.END_LEVEL,
					() =>
					{
						LevelManager.Instance.ToLevelSelect();
					}
				);
		}

		private void ShowNextLevelPopup()
		{
			Popup.Instance.OpenPopup(
					NextLevelBodyText,
					PopupType.END_LEVEL,
					() =>
					{
						LevelManager.Instance.CurrentLevel = LevelManager.Instance.CurrentLevel.nextLevel;
						LevelManager.Instance.ToGameplay();
					}
				);
		}

		private bool AreAllWordsMatching(string[] shelfWords, WordData[] wordDatas)
		{
			foreach (WordData wordData in wordDatas)
			{
				bool foundMatch = false;
				string correctWord = wordData.TranslatedWord;

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

		private bool IsLevelUndefined(Level levelConfiguration)
		{
			if (levelConfiguration == null)
			{
				Debug.LogError(UndefinedLevelErrorMessage);
				return true;
			}

			return false;
		}
	}
}