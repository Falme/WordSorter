using UnityEngine;

public class GameManager : MonoBehaviour
{
	private Shelf lastSelectedShelf = null;
	// List<PanelWord> panelWords

	private void OnEnable() => Shelf.SelectShelfEvent += OnSelectedShelf;
	private void OnDisable() => Shelf.SelectShelfEvent -= OnSelectedShelf;

	private void OnSelectedShelf(Shelf shelf)
	{
		if (lastSelectedShelf == null && shelf.CanDetachBlock())
		{
			shelf.Highlight();
			lastSelectedShelf = shelf;
		}
		else if (lastSelectedShelf != null && shelf.CanAppendBlock())
		{
			PassBlockToShelf(lastSelectedShelf, shelf);
			lastSelectedShelf = null;
		}
	}

	private void PassBlockToShelf(Shelf shelfFrom, Shelf shelfTo)
	{
		shelfTo.AppendBlock(shelfFrom.DetachBlock());
	}
}
