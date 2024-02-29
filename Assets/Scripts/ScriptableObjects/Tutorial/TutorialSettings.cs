using UnityEngine;

namespace WordSorter
{
	[CreateAssetMenu(fileName = "TutorialSettings", menuName = "ScriptableObject/TutorialSettings")]
	public class TutorialSettings : ScriptableObject
	{
		public Level tutorialLevel;
		public string[] instructionsMessages;
	}
}