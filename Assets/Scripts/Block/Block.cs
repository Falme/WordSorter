using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
	private TextMeshPro text;
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
		text = GetComponentInChildren<TextMeshPro>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void Highlight(bool enable)
    {
		if(enable) spriteRenderer.color = Color.blue;
		else spriteRenderer.color = Color.white;
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
