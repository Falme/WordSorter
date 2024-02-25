using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
	private TextMeshPro text;
	private SpriteRenderer spriteRenderer;
	private Color32 initialColor;

	private char letter = '?';
	public char Letter
	{
		get => letter;
		set
		{
			letter = value;
			PrintLetter();
		}
	}

	private void Awake()
	{
		text = GetComponentInChildren<TextMeshPro>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		initialColor = spriteRenderer.color;
	}

	public void Highlight(bool enable)
	{
		spriteRenderer.color = enable ? Color.blue : initialColor;
	}

	public void MoveTo(Vector3 newPosition)
	{
		transform.position = newPosition;
	}

	private void PrintLetter()
	{
		text.text = Letter.ToString();
	}
}
