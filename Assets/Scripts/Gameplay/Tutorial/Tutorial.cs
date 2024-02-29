using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class Tutorial : MonoBehaviour
	{
		[SerializeField] private Level tutorialLevel;
		[SerializeField] private TextMeshProUGUI instructionsText;


		private void Start()
		{
			if (tutorialLevel.Equals(LevelManager.Instance.CurrentLevel))
				StartTutorial();
		}

		private void StartTutorial()
		{
			Debug.Log("Starting Tutorial");
		}

		private void WriteNextInstruction()
		{
			instructionsText.text = Localization.GetLocalizedMessage("yes");
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1)) WriteNextInstruction();
		}
	}
}
