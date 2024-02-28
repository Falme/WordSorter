using TMPro;
using UnityEngine;

public class WordItem : MonoBehaviour
{
	[SerializeField] private WordItemSettings settings;
	private TextMeshProUGUI text;
	public string Word { get; private set; }

	public void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	public void Initialize(string word)
	{
		text.text = Word = word;
		Highlight(false);
	}

	public void Highlight(bool enable)
	{
		text.fontStyle = enable ? settings.highlightFontStyle : settings.defaultFontStyle;
		text.color = enable ? settings.highlightColor : settings.defaultColor;
	}

}
