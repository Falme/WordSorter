using System.Collections.Generic;
using UnityEngine;

namespace WordSorter
{
	public class PanelWords : MonoBehaviour
	{
		[SerializeField] private GameObject wordItemPrefab;
		[SerializeField] private Transform topArea, bottomArea;

		private Level level;
		private List<WordItem> wordItems = new List<WordItem>();

		private void OnEnable() => ShelfManager.CompareWordsEvent += CompareWords;
		private void OnDisable() => ShelfManager.CompareWordsEvent -= CompareWords;

		public void Initialize(Level level)
		{
			this.level = level;
			InstantiateWordItems();
		}

		public void InstantiateWordItems()
		{
			foreach (WordData wordData in level.shelvesData)
			{
				if (string.IsNullOrEmpty(wordData.word)) continue;

				wordItems.Add(Instantiate(wordItemPrefab, topArea).GetComponent<WordItem>());
				wordItems[wordItems.Count - 1].Initialize(wordData.word);
			}
		}

		private void CompareWords(string[] words)
		{
			ClearWords();

			foreach (WordItem wordItem in wordItems)
			{
				if (FoundMatch(words, wordItem.Word))
					wordItem.Highlight(true);
			}
		}

		private bool FoundMatch(string[] items, string word)
		{
			foreach (string item in items)
			{
				if (word.Equals(item))
					return true;
			}

			return false;
		}

		public void ClearWords()
		{
			foreach (WordItem wordItem in wordItems)
				wordItem?.Highlight(false);
		}

		public void Restart() => ClearWords();

	}
}