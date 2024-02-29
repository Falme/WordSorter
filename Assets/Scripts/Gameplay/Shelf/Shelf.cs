using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WordSorter
{
	public class Shelf : MonoBehaviour
	{

		private const string WordBiggerThanShelfMessage = "Number of Letters in a Shelf was Exceeded at GenerateBlocks \n Make Sure to not put a longer word to a smaller shelf";

		#region Events
		public delegate void SelectShelfDelegate(Shelf shelf);
		public static event SelectShelfDelegate SelectShelfEvent;

		#endregion


		[SerializeField] private Block blockPrefab;
		[SerializeField] private Transform blocksArea, board;

		private Block[] blocks;
		private int nextBlockIndex = 0;
		private float blockSpacing;

		private string originalScrambledWord;

		public bool CanAppendBlock => blocks[blocks.Length - 1] == null;
		public bool CanDetachBlock => blocks[0] != null;

		public int ShelfLength => nextBlockIndex;

		public void Initialize(ShelfSettings shelfSettings, Level level, int index)
		{
			InitializeBlockGeneration(shelfSettings, level, index);
			InitializeBoard(shelfSettings, level);
		}

		private void InitializeBlockGeneration(ShelfSettings shelfSettings, Level levelConfiguration, int index)
		{
			blockSpacing = shelfSettings.blockSpacing;

			var scrambledWord = levelConfiguration.shelvesData[index].scrambledWord;
			var capacity = levelConfiguration.shelfCapacity;

			originalScrambledWord = scrambledWord;
			StartCoroutine(GenerateBlocks(scrambledWord, capacity));
		}

		private void InitializeBoard(ShelfSettings shelfSettings, Level levelConfiguration)
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
				if (blocks[a] != null)
					result.Append(blocks[a].Letter);

			return result.ToString();
		}

		private bool IsWordBiggerThanShelf(string word, int shelfSize)
		{
			if (word.Length > shelfSize)
			{
				Debug.LogError(WordBiggerThanShelfMessage);
				return true;
			}
			return false;
		}

		public void Restart()
		{
			for (int a = 0; a < blocks.Length; a++)
			{
				if (blocks[a] != null)
					blocks[a].Letter = originalScrambledWord[a];
			}
		}

		public void OnMouseDown()
		{
			if (!EventSystem.current.IsPointerOverGameObject())
				SelectShelf();
		}

	}
}