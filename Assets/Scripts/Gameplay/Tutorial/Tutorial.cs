using System;
using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class Tutorial : MonoBehaviour
	{
		[Header("Prefab References")]
		[SerializeField] private TextMeshProUGUI instructionsText;
		[SerializeField] private TutorialSettings tutorialSettings;

		[Header("Scene References")]
		[SerializeField] private ShelfManager shelfManager;

		private Animator animator;

		private int currentInstruction = -1;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		public void Initialize(Level level)
		{
			if (tutorialSettings.tutorialLevel.Equals(level))
				StartTutorial();
		}

		private void StartTutorial()
		{
			WriteNextInstruction();
		}

		private void WriteNextInstruction()
		{
			if (currentInstruction >= tutorialSettings.tutorialSteps.Length - 1) return;

			currentInstruction++;

			shelfManager.EnableShelfInteraction(tutorialSettings.tutorialSteps[currentInstruction].enableShelvesInteraction);

			string message = tutorialSettings.tutorialSteps[currentInstruction].instructionsMessages;
			instructionsText.text = Localization.GetLocalizedMessage(message);

			animator.SetTrigger("NextAnimation");

		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				WriteNextInstruction();
			}
		}

	}
}
