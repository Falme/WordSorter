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
		private bool currentlyInTutorial = false;

		private void Awake()
		{
			animator = GetComponent<Animator>();
		}

		private void OnEnable() => ShelfManager.CompareWordsEvent += CheckInteractiveStep;
		private void OnDisable() => ShelfManager.CompareWordsEvent -= CheckInteractiveStep;

		public void Initialize(Level level)
		{
			if (tutorialSettings.tutorialLevel.Equals(level))
				StartTutorial();
		}

		private void StartTutorial()
		{
			currentlyInTutorial = true;
			WriteNextInstruction();
		}

		private void WriteNextInstruction()
		{
			if (currentInstruction >= tutorialSettings.tutorialSteps.Length - 1) return;

			currentInstruction++;
			ShelvesInteraction();
			WriteInstructionsText();
			NextAnimation();

		}

		private void NextAnimation()
		{
			animator.SetTrigger("NextAnimation");
		}

		private void WriteInstructionsText()
		{
			string message = tutorialSettings.tutorialSteps[currentInstruction].instructionsMessages;
			instructionsText.text = Localization.GetLocalizedMessage(message);
		}

		private void ShelvesInteraction()
		{
			shelfManager.EnableShelfInteraction(tutorialSettings.tutorialSteps[currentInstruction].enableShelvesInteraction);
		}

		public void Update()
		{
			if (!currentlyInTutorial) return;

			if (Input.GetMouseButtonDown(0))
			{
				if (!tutorialSettings.tutorialSteps[currentInstruction].interactiveStep)
					WriteNextInstruction();
			}
		}

		private void CheckInteractiveStep(string[] wordsInShelf)
		{
			if (wordsInShelf[0].Length == tutorialSettings.tutorialSteps[currentInstruction].validFirstShelfLength)
				WriteNextInstruction();
		}

	}
}
