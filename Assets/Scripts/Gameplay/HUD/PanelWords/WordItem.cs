using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class WordItem : MonoBehaviour
	{
		[SerializeField] private WordItemSettings settings;
		private TextMeshProUGUI text;
		public string Word { get; private set; }
		public bool Active { get; set; }

		public void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		public void Initialize(string newWord)
		{
			Active = true;

			Word = newWord;
			text.SetText(newWord);

			Highlight(false);
		}

		public void Highlight(bool enable)
		{
			text.fontStyle = enable ? settings.highlightFontStyle : settings.defaultFontStyle;
			text.color = enable ? settings.highlightColor : settings.defaultColor;
		}

	}
}