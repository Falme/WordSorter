using UnityEngine;

namespace WordSorter
{
	public class Score : MonoBehaviour
	{
		[SerializeField] private Star[] stars;

		public void ShowResult(int rateScore)
		{
			int quantity = 1;

			if (rateScore == LevelManager.Instance.CurrentLevel.minimumMoves) quantity = 3;
			else if (rateScore <= (LevelManager.Instance.CurrentLevel.minimumMoves * 2)) quantity = 2;

			SaveFinalScore(quantity);

			for (int a = 0; a < quantity; a++)
				stars[a].Show(true);
		}

		public void ShowAbsoluteResult(int rateScore)
		{
			for (int a = 0; a < rateScore; a++)
				stars[a].Show(true);
		}

		public void Restart()
		{
			for (int a = 0; a < stars.Length; a++)
				stars[a].Restart();
		}

		private void SaveFinalScore(int quantity)
		{
			string key = $"SCORE_{LevelManager.Instance.CurrentWorld}_{LevelManager.Instance.CurrentLevel.index}";

			if (PlayerPrefs.HasKey(key))
			{
				if (PlayerPrefs.GetInt(key) <= quantity)
					PlayerPrefs.SetInt(key, quantity);
			}
			else
			{
				PlayerPrefs.SetInt(key, quantity);
			}
		}

		public void ShowLevelScore(int currentWorld, Level currentLevel)
		{
			Restart();

			string key = $"SCORE_{currentWorld}_{currentLevel.index}";
			if (PlayerPrefs.HasKey(key))
				ShowAbsoluteResult(PlayerPrefs.GetInt(key));
		}
	}
}