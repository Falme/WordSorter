using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
	[Header("Prefab Reference Values")]
	[SerializeField] private BlockSettings blockSettings;

	private SpriteRenderer spriteRenderer;

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
		spriteRenderer = GetComponent<SpriteRenderer>();

		Highlight(false);
	}

	public void Highlight(bool enable)
	{
		spriteRenderer.color = enable ? blockSettings.highlightColor : blockSettings.defaultColor;
	}

	public void MoveTo(Vector3 newPosition, Transform newParent)
	{
		transform.SetParent(newParent);
		transform.localPosition = newPosition;
	}

	private void PrintLetter()
	{
		GetComponentInChildren<TextMeshPro>().text = Letter.ToString();
	}
}
