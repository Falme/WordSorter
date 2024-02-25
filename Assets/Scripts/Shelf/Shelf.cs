using System.Collections;
using System.Text;
using UnityEngine;

public class Shelf : MonoBehaviour
{
	#region Events
	public delegate void SelectShelfDelegate(Shelf shelf);
	public static event SelectShelfDelegate SelectShelfEvent;
	#endregion

	[Header("Prefab Reference Values")]
	[SerializeField] private Block blockPrefab;
	[SerializeField] private Transform blocksArea, board;

	private Block[] blocks;
	private int nextBlockIndex = 0;
	private float blockSpacing;

	public bool CanAppendBlock => blocks[blocks.Length - 1] == null;
	public bool CanDetachBlock => blocks[0] != null;

	public void Initialize(ShelfSettings shelfSettings, LevelConfiguration levelConfiguration, int index)
	{
		InitializeBlockGeneration(shelfSettings, levelConfiguration, index);
		InitializeBoard(shelfSettings, levelConfiguration);
	}

	private void InitializeBlockGeneration(ShelfSettings shelfSettings, LevelConfiguration levelConfiguration, int index)
	{
		blockSpacing = shelfSettings.blockSpacing;

		var scrambledWord = levelConfiguration.shelvesData[index].scrambledWord;
		var capacity = levelConfiguration.shelfCapacity;
		StartCoroutine(GenerateBlocks(scrambledWord, capacity));
	}

	private void InitializeBoard(ShelfSettings shelfSettings, LevelConfiguration levelConfiguration)
	{
		var capacity = levelConfiguration.shelfCapacity;
		SetBoardWidth((capacity - 1) * shelfSettings.widthIncrement);
	}

	private void SetBoardWidth(float xPosition)
	{
		board.localPosition += Vector3.right * xPosition;
	}

	public IEnumerator GenerateBlocks(string word, int shelfSize)
	{
		if (IsWordBiggerThanShelf(word, shelfSize)) yield break;

		blocks = new Block[shelfSize];

		foreach (char letter in word)
		{
			var block = Instantiate(blockPrefab.gameObject, blocksArea).GetComponent<Block>();
			block.Letter = letter;

			AppendBlock(block);

			yield return null;
		}
	}

	public void AppendBlock(Block appendBlock)
	{
		appendBlock.Highlight(false);
		appendBlock.MoveTo(GetPositionOfNextBlock(), blocksArea);

		blocks[nextBlockIndex] = appendBlock;

		nextBlockIndex++;
	}

	public Block DetachBlock()
	{
		var detachBlock = blocks[nextBlockIndex - 1];
		blocks[nextBlockIndex - 1] = null;

		nextBlockIndex--;

		return detachBlock;
	}

	public void SelectShelf()
	{
		SelectShelfEvent?.Invoke(this);
	}

	public void Highlight()
	{
		blocks[nextBlockIndex - 1].Highlight(true);
	}

	public Vector3 GetPositionOfNextBlock()
	{
		return new Vector3(blockSpacing * nextBlockIndex, 0f);
	}

	public string Stringify()
	{
		StringBuilder result = new StringBuilder();

		for (int a = 0; a < blocks.Length; a++)
			result.Append(blocks[a].Letter);

		return result.ToString();
	}

	private bool IsWordBiggerThanShelf(string word, int shelfSize)
	{
		if (word.Length > shelfSize)
		{
			Debug.LogError("Number of Letters in a Shelf was Exceeded at GenerateBlocks \n " +
							"Make Sure to not put a longer word to a smaller shelf");
			return true;
		}
		return false;
	}

	public void OnMouseDown() => SelectShelf();

}