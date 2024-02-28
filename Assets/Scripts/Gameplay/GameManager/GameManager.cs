using UnityEngine;

public class GameManager : MonoBehaviour
{
	[Header("Scene References")]
	[SerializeField] private ShelfManager shelfManager;
	[SerializeField] private PanelWords panelWords;

	private LevelConfiguration levelConfiguration;

	private void Start()
	{
		levelConfiguration = LevelManager.Instance.CurrentLevel;

		if (levelConfiguration == null)
		{
			Debug.LogError("LevelConfiguration was not set, please check " +
							"the Level Selection and Level Scriptable Objects");
			return;
		}

		shelfManager.Initialize(levelConfiguration);
		panelWords.Initialize(levelConfiguration);
	}

	private void OnEnable() => ShelfManager.CompareWordsEvent += CompareWords;
	private void OnDisable() => ShelfManager.CompareWordsEvent -= CompareWords;

	private void CompareWords(string[] words)
	{
		Debug.Log(CheckIfAllWordsMatch(words, levelConfiguration.shelvesData));
	}

	private bool CheckIfAllWordsMatch(string[] shelfWords, WordData[] correctWords)
	{
		for (int a = 0; a < correctWords.Length; a++)
		{
			bool found = false;
			for (int b = 0; b < shelfWords.Length; b++)
				if (correctWords[a].word.Equals(shelfWords[b]))
					found = true;

			if (!found) return false;
		}

		return true;
	}

	public void Restart()
	{
		shelfManager.Restart();
		panelWords.Restart();
	}
}
