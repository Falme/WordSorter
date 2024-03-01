using System;
using UnityEngine;

namespace WordSorter
{
	[Flags]
	public enum ShelfMask
	{
		One = 1,
		Two = 2,
		Three = 4,
		Four = 8,
		Five = 16
	}

	[CreateAssetMenu(fileName = "TutorialSettings", menuName = "ScriptableObject/TutorialSettings")]
	public class TutorialSettings : ScriptableObject
	{
		public Level tutorialLevel;
		public TutorialStep[] tutorialSteps;
	}

	[Serializable]
	public class TutorialStep
	{
		public string instructionsMessages;
		public ShelfMask enableShelvesInteraction;
	}
}