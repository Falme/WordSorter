using UnityEngine;

namespace WordSorter
{
	public class PanelWords : MonoBehaviour
	{
		[SerializeField] private GameObject wordItemPrefab;
		[SerializeField] private Transform topArea;
		[SerializeField] private WordItem[] wordItems;

		private Level level;

		private void OnEnable() => ShelfManager.CompareWordsEvent += CompareWords;
		private void OnDisable() => ShelfManager.CompareWordsEvent -= CompareWords;

		public void Initialize(Level level)
		{
			this.level = level;
			InstantiateWordItems();
		}

		public void InstantiateWordItems()
		{
			for (int i = 0; i < level.shelvesData.Length; i++)
			{
				WordData wordData = level.shelvesData[i];
				if (string.IsNullOrEmpty(wordData.word)) continue;

				wordItems[i].Initialize(wordData.TranslatedWord);
			}
		}

		private void CompareWords(string[] words)
		{
			Restart();

			foreach (WordItem wordItem in wordItems)
			{
				if (wordItem.Active && FoundMatch(words, wordItem.Word))
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

		public void Restart()
		{
			foreach (WordItem wordItem in wordItems)
				wordItem?.Highlight(false);
		}
	}
}