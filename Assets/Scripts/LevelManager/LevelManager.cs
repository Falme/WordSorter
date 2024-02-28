using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	public LevelConfiguration CurrentLevel { get; set; }
	public LevelConfiguration NextLevel { get; set; }

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

	public void ChangeToNextLevel()
	{
		CurrentLevel = NextLevel;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
