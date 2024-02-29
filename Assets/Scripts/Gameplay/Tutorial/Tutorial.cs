using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class Tutorial : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI instructionsText;
		[SerializeField] private TutorialSettings tutorialSettings;

		private int currentInstruction = -1;

		private void Start()
		{
			if (tutorialSettings.tutorialLevel.Equals(LevelManager.Instance.CurrentLevel))
				StartTutorial();
		}

		private void StartTutorial()
		{
			Debug.Log("Starting Tutorial");
			WriteNextInstruction();
		}

		private void WriteNextInstruction()
		{
			if (currentInstruction >= tutorialSettings.instructionsMessages.Length - 1) return;

			currentInstruction++;
			instructionsText.text = Localization.GetLocalizedMessage(tutorialSettings.instructionsMessages[currentInstruction]);
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1)) WriteNextInstruction();
		}
	}
}
