using UnityEngine;

namespace WordSorter
{
	[CreateAssetMenu(fileName = "ScoreSettings", menuName = "ScriptableObject/ScoreSettings")]
	public class ScoreSettings : ScriptableObject
	{
		public Color defaultColor, highlightColor;
	}
}