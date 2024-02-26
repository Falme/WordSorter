using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelWords : MonoBehaviour
{
	[SerializeField] private GameObject wordItemPrefab;
	[SerializeField] private Transform wordAreaTop, wordAreaBottom;

	private LevelConfiguration levelConfiguration;
	private List<WordItem> wordItems;

	private void OnEnable() => ShelfManager.CompareWordsEvent += CompareWords;
	private void OnDisable() => ShelfManager.CompareWordsEvent -= CompareWords;

	public void Initialize(LevelConfiguration levelConfiguration)
	{
		this.levelConfiguration = levelConfiguration;
		GenerateWordItems();
	}

	public void GenerateWordItems()
	{
		wordItems = new List<WordItem>();

		for (int a = 0; a < levelConfiguration.shelvesData.Length; a++)
		{
			if (levelConfiguration.shelvesData[a].word.Equals(string.Empty)) continue;

			wordItems.Add(Instantiate(wordItemPrefab, wordAreaTop).GetComponent<WordItem>());
			wordItems[wordItems.Count - 1].Initialize(levelConfiguration.shelvesData[a].word);
		}
	}

	private void CompareWords(string[] words)
	{
		ClearWords();

		for (int a = 0; a < wordItems.Count; a++)
		{
			for (int b = 0; b < words.Length; b++)
			{
				if (wordItems[a].Word.Equals(words[b]))
				{
					wordItems[a].Highlight(true);
					break;
				}
			}
		}
	}

	public void ClearWords()
	{
		for (int a = 0; a < wordItems.Count; a++)
		{
			if (wordItems[a] != null)
				wordItems[a].Highlight(false);
		}
	}

	public void Restart()
	{
		ClearWords();
	}

}
