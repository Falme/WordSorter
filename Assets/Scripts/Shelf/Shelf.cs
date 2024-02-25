using System.Collections;
using System.Text;
using UnityEngine;

public class Shelf : MonoBehaviour
{
	public delegate void SelectShelfDelegate(Shelf shelf);
	public static event SelectShelfDelegate SelectShelfEvent;

	[Header("Prefab Reference Values")]
	[SerializeField] private Block blockPrefab;
	[SerializeField] private Transform shelfBlocks;
	[SerializeField] private ShelfSettings shelfSettings;

	[Header("Shelf Generation Data")]
	[SerializeField] private string word;
	[SerializeField] private int shelfSize;

	private Block[] blocks;
	private int blockPositionIndex = 0;

	public Vector3 PositionOfNextLetter =>
		new Vector3(shelfSettings.letterSpacing * blockPositionIndex, 0f);

	public bool IsFilled => blocks[blocks.Length - 1] != null;
	public bool IsEmpty => blocks[0] == null;

	public bool CanAppendBlock => !IsFilled;
	public bool CanDetachBlock => !IsEmpty;

	private void Start() => StartCoroutine(GenerateBlocks(word, shelfSize));

	public void OnMouseDown() => SelectShelf();

	public IEnumerator GenerateBlocks(string _word, int _shelfSize)
	{
		if (IsWordBiggerThanShelf(_word, _shelfSize)) yield break;

		_word = _word.ToUpper();

		blocks = new Block[_shelfSize];

		foreach (char letter in _word)
		{
			var _block = Instantiate(blockPrefab.gameObject, shelfBlocks).GetComponent<Block>();
			_block.Letter = letter;

			AppendBlock(_block);

			yield return null;
		}
	}

	public void SelectShelf()
	{
		SelectShelfEvent?.Invoke(this);
	}

	public void Highlight()
	{
		blocks[blockPositionIndex - 1].Highlight(true);
	}


	public void AppendBlock(Block block)
	{
		block.Highlight(false);

		block.transform.SetParent(shelfBlocks);
		block.transform.localPosition = PositionOfNextLetter;

		blocks[blockPositionIndex] = block;

		blockPositionIndex++;
	}

	public Block DetachBlock()
	{
		var _block = blocks[blockPositionIndex - 1];
		blocks[blockPositionIndex - 1] = null;

		blockPositionIndex--;

		return _block;
	}

	public string Stringify()
	{
		StringBuilder _result = new StringBuilder();

		for (int a = 0; a < blocks.Length; a++)
			_result.Append(blocks[a].Letter);

		return _result.ToString();
	}

	private bool IsWordBiggerThanShelf(string _word, int _shelfSize)
	{
		if (_word.Length > _shelfSize)
		{
			Debug.LogError("Number of Letters in a Shelf was Exceeded at GenerateBlocks \n " +
							"Make Sure to not put a longer word to a smaller shelf");
			return true;
		}
		return false;
	}


}