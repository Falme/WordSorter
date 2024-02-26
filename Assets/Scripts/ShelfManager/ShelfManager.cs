using System;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{

	#region Events
	public delegate void CompareWordsDelegate(string[] words);
	public static event CompareWordsDelegate CompareWordsEvent;
	#endregion

	[SerializeField] private GameObject shelfPrefab;
	[SerializeField] private ShelfSettings shelfSettings;

	private LevelConfiguration levelConfiguration;

	private Shelf lastSelectedShelf = null;

	private Shelf[] shelves;

	public void Initialize(LevelConfiguration levelConfiguration)
	{
		this.levelConfiguration = levelConfiguration;
		InstantiateShelves();
	}

	private void OnEnable() => Shelf.SelectShelfEvent += OnSelectedShelf;
	private void OnDisable() => Shelf.SelectShelfEvent -= OnSelectedShelf;

	private void InstantiateShelves()
	{
		shelves = new Shelf[levelConfiguration.shelvesData.Length];

		for (int i = 0; i < shelves.Length; i++)
		{
			var shelf = Instantiate(shelfPrefab, transform).GetComponent<Shelf>();
			shelf.transform.localPosition = new Vector3(0f, i * -shelfSettings.verticalSpacing);

			shelf.Initialize(shelfSettings, levelConfiguration, i);

			shelves[i] = shelf;
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
		else if (lastSelectedShelf != null && shelf.CanAppendBlock)
		{
			PassBlockToShelf(lastSelectedShelf, shelf);
			lastSelectedShelf = null;
		}

		CompareWordsEvent?.Invoke(GetShelvesContent());
	}

	private void PassBlockToShelf(Shelf shelfFrom, Shelf shelfTo)
	{
		shelfTo.AppendBlock(shelfFrom.DetachBlock());
	}

	public void Restart()
	{
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
			int scrambledWordLength = levelConfiguration.shelvesData[a].scrambledWord.Length;

			if (shelfLength > scrambledWordLength)
				unbalancedShelves[0] = shelves[a];
			else if (shelfLength < scrambledWordLength)
				unbalancedShelves[1] = shelves[a];
		}

		return unbalancedShelves;
	}

}
