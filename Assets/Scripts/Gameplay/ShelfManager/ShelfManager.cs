using UnityEngine;

namespace WordSorter
{
	public class ShelfManager : MonoBehaviour
	{

		#region Events
		public delegate void CompareWordsDelegate(string[] words);
		public static event CompareWordsDelegate CompareWordsEvent;

		public delegate void SuccessfulMoveDelegate();
		public static event SuccessfulMoveDelegate SuccessfulMoveEvent;
		#endregion

		[SerializeField] private GameObject shelfPrefab;
		[SerializeField] private ShelfSettings shelfSettings;

		private Level level;

		private Shelf lastSelectedShelf = null;

		private Shelf[] shelves;

		public void Initialize(Level level)
		{
			this.level = level;
			InstantiateShelves();
		}

		private void OnEnable() => Shelf.SelectShelfEvent += OnSelectedShelf;
		private void OnDisable() => Shelf.SelectShelfEvent -= OnSelectedShelf;

		private void InstantiateShelves()
		{
			shelves = new Shelf[level.shelvesData.Length];

			for (int a = 0; a < shelves.Length; a++)
			{
				var shelf = Instantiate(shelfPrefab, transform).GetComponent<Shelf>();
				shelf.transform.localPosition = new Vector3(0f, a * -shelfSettings.verticalSpacing);

				shelf.Initialize(shelfSettings, level, a);

				shelves[a] = shelf;
			}
		}

		private string[] GetShelvesContent()
		{
			string[] words = new string[shelves.Length];

			for (int a = 0; a < words.Length; a++)
				words[a] = shelves[a].Stringify();

			return words;
		}

		private void OnSelectedShelf(Shelf shelf)
		{
			if (lastSelectedShelf == null && shelf.CanDetachBlock)
			{
				shelf.Highlight();
				lastSelectedShelf = shelf;
			}
			else if (lastSelectedShelf != null && !lastSelectedShelf.Equals(shelf) && shelf.CanAppendBlock)
			{
				PassBlockToShelf(lastSelectedShelf, shelf);
				lastSelectedShelf = null;
				SuccessfulMoveEvent?.Invoke();
			}

			CompareWordsEvent?.Invoke(GetShelvesContent());
		}

		private void PassBlockToShelf(Shelf shelfFrom, Shelf shelfTo)
		{
			shelfTo.AppendBlock(shelfFrom.DetachBlock());
		}

		public void Restart()
		{
			lastSelectedShelf = null;

			Shelf[] unbalancedShelves = GetUnbalancedShelves();
			while (unbalancedShelves[0] != null)
			{
				PassBlockToShelf(unbalancedShelves[0], unbalancedShelves[1]);
				unbalancedShelves = GetUnbalancedShelves();
			}

			for (int a = 0; a < shelves.Length; a++)
				shelves[a].Restart();
		}

		private Shelf[] GetUnbalancedShelves()
		{
			// First Value is Higher
			// Second Value is Lower
			Shelf[] unbalancedShelves = new Shelf[2];

			for (int a = 0; a < shelves.Length; a++)
			{
				int shelfLength = shelves[a].ShelfLength;
				int scrambledWordLength = level.shelvesData[a].TranslatedScrambledWord.Length;

				if (shelfLength > scrambledWordLength) unbalancedShelves[0] = shelves[a];
				if (shelfLength < scrambledWordLength) unbalancedShelves[1] = shelves[a];
			}

			return unbalancedShelves;
		}

		public void EnableShelfInteraction(ShelfMask shelfMask)
		{
			//mask is a bitmask that bitshift (^2) each array index
			for (int a = 0, mask = 1; a < shelves.Length; a++)
			{
				shelves[a].SetInteraction((mask & (int)shelfMask) == mask);
				shelfMask = (ShelfMask)((int)shelfMask >> 1);
			}
		}
	}
}