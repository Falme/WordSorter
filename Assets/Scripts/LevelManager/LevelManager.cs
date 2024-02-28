using UnityEngine;
using UnityEngine.SceneManagement;

namespace WordSorter
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		public LevelConfiguration CurrentLevel { get; set; }

#if UNITY_EDITOR

		[SerializeField] private LevelConfiguration debugLevel;

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

		public void LoadScene(string sceneName)
		{
			SceneManager.LoadScene(sceneName);
		}
	}
}