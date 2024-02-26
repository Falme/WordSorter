using System;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{

	#region Events
	public delegate void SendCurrentWordsDelegate(string[] words);
	public static event SendCurrentWordsDelegate SendCurrentWordsEvent;
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

		SendCurrentWordsEvent?.Invoke(GetShelvesContent());
	}

	private void PassBlockToShelf(Shelf shelfFrom, Shelf shelfTo)
	{
		shelfTo.AppendBlock(shelfFrom.DetachBlock());
	}

}
