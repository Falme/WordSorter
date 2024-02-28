using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class Block : MonoBehaviour
	{
		[Header("Prefab Reference Values")]
		[SerializeField] private BlockSettings blockSettings;

		private SpriteRenderer spriteRenderer;
		private TextMeshPro letterText;

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
			letterText = GetComponentInChildren<TextMeshPro>();

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
			letterText.text = Letter.ToString();
		}
	}
}