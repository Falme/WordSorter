using UnityEngine;

namespace WordSorter
{
	public class Tutorial : MonoBehaviour
	{
		[SerializeField] private Level tutorialLevel;

		private void Start()
		{
			if (tutorialLevel.Equals(LevelManager.Instance.CurrentLevel))
				StartTutorial();
		}

		private void StartTutorial()
		{
			Debug.Log("Starting Tutorial");
		}
	}
}
