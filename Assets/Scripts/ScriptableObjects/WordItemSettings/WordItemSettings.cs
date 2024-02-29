using TMPro;
using UnityEngine;

namespace WordSorter
{
	[CreateAssetMenu(fileName = "WordItemSettings", menuName = "ScriptableObject/WordItemSettings")]
	public class WordItemSettings : ScriptableObject
	{
		public Color defaultColor, highlightColor;
		public FontStyles defaultFontStyle, highlightFontStyle;
	}
}