using TMPro;
using UnityEngine;

public class WordItem : MonoBehaviour
{
	private TextMeshProUGUI text;
	public string Word { get; private set; }

	public void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
	}

	public void Initialize(string word)
	{
		Word = word;
		Print();
	}

	private void Print()
	{
		text.text = Word;
	}

	public void Highlight(bool enable)
	{
		text.fontStyle = enable ? (FontStyles.Bold | FontStyles.Underline) : FontStyles.Normal;
	}

}
