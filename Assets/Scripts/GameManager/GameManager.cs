using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private LevelConfiguration levelConfiguration;
	[SerializeField] private ShelfManager shelfManager;
	[SerializeField] private PanelWords panelWords;

	private void Start()
	{
		shelfManager.Initialize(levelConfiguration);
		panelWords.Initialize(levelConfiguration);
	}

	private void OnEnable() => ShelfManager.SendCurrentWordsEvent += OnSelectedShelf;
	private void OnDisable() => ShelfManager.SendCurrentWordsEvent -= OnSelectedShelf;

	private void OnSelectedShelf(string[] words)
	{
		Debug.Log(CheckIfAllWordsMatch(words, levelConfiguration.shelvesData));
	}

	private bool CheckIfAllWordsMatch(string[] shelfWords, WordData[] correctWords)
	{
		for (int a = 0; a < correctWords.Length; a++)
		{
			bool found = false;
			for (int b = 0; b < shelfWords.Length; b++)
			{
				if (correctWords[a].word.Equals(shelfWords[b])) found = true;
			}

			if (!found) return false;
		}

		return true;
	}
}
