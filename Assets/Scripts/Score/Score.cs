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

			for (int a = 0; a < quantity; a++)
				stars[a].Show(true);
		}

		public void Restart()
		{
			for (int a = 0; a < stars.Length; a++)
				stars[a].Restart();
		}
	}
}