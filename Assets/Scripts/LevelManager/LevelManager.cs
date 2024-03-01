using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordSorter
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		public Level CurrentLevel { get; set; }
		public int CurrentWorld { get; set; }

		private const string LevelSelect = "LevelSelect";
		private const string Gameplay = "Gameplay";

#if UNITY_EDITOR

		[SerializeField] private Level debugLevel;

		private void Start()
		{
			if (debugLevel != null)
				CurrentLevel = debugLevel;
		}

#endif

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		private void OnEnable() => LevelList.ChangedWorldNumberEvent += UpdateCurrentWorld;

		private void OnDisable() => LevelList.ChangedWorldNumberEvent -= UpdateCurrentWorld;

		private void UpdateCurrentWorld(int currentWorld, int worldsLength)
		{
			CurrentWorld = currentWorld;
		}

		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}

		public void ToGameplay() => LoadScene(Gameplay);
		public void ToLevelSelect() => LoadScene(LevelSelect);
	}
}