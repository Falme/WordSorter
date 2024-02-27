using UnityEngine;

public class LevelList : MonoBehaviour
{
	[SerializeField] private LevelSelectionConfiguration levelSelectionConfiguration;

	private LevelButton[] levelButtons;
	private int world = 0;

	private void Start()
	{
		LoadWorld();
	}

	public void LoadWorld()
	{
		levelButtons = new LevelButton[transform.childCount];

		for (int a = 0; a < levelButtons.Length; a++)
			levelButtons[a] = transform.GetChild(a).GetComponent<LevelButton>();

		UpdateLevels();
	}

	public void UpdateLevels()
	{
		for (int a = 0; a < levelButtons.Length; a++)
		{
			if (a >= levelSelectionConfiguration.worlds[world].levels.Length)
			{
				levelButtons[a].ChangeStatus(false);
				continue;
			}
			levelButtons[a].ChangeStatus(levelSelectionConfiguration.worlds[world].levels[a].isEnabled);
			levelButtons[a].SceneName = levelSelectionConfiguration.worlds[world].levels[a].sceneName;
		}

	}

	public void NextWorld()
	{
		world++;
		UpdateLevels();
	}

	public void PreviousWorld()
	{
		world--;
		UpdateLevels();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q)) PreviousWorld();
		if (Input.GetKeyDown(KeyCode.W)) NextWorld();
	}
}
