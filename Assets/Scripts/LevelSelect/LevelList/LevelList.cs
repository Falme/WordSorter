using UnityEngine;

namespace WordSorter
{
	public class LevelList : MonoBehaviour
	{

		#region Events
		public delegate void SelectedWorldTitleDelegate(string worldName);
		public static event SelectedWorldTitleDelegate SelectedWorldTitleEvent;

		public delegate void ChangedWorldNumberDelegate(int currentWorld, int worldsLength);
		public static event ChangedWorldNumberDelegate ChangedWorldNumberEvent;
		#endregion


		[SerializeField] private LevelSelectionConfiguration levelSelectionConfiguration;

		private LevelButton[] levelButtons;
		private int world = 0;

		private void Start()
		{
			LevelManager.Instance.CurrentWorld = 0;

			LoadWorld();
			UpdateUI();
		}

		public void LoadWorld()
		{
			levelButtons = new LevelButton[transform.childCount];

			for (int a = 0; a < levelButtons.Length; a++)
				levelButtons[a] = transform.GetChild(a).GetComponent<LevelButton>();

			UpdateLevels();
		}

		private void UpdateUI()
		{
			SelectedWorldTitleEvent?.Invoke(levelSelectionConfiguration.worlds[world].worldName);
			ChangedWorldNumberEvent?.Invoke(world, levelSelectionConfiguration.worlds.Length);
		}

		public void UpdateLevels()
		{
			for (int a = 0; a < levelButtons.Length; a++)
			{
				if (a >= levelSelectionConfiguration.worlds[world].levels.Length)
				{
					levelButtons[a].HideScore();
					levelButtons[a].ChangeStatus(false);
					continue;
				}
				levelButtons[a].ChangeStatus(levelSelectionConfiguration.worlds[world].levels[a].isEnabled);
				levelButtons[a].LevelData = levelSelectionConfiguration.worlds[world].levels[a].levelData;
				levelButtons[a].ShowScore();
			}

		}

		public void NextWorld()
		{
			if (world >= levelSelectionConfiguration.worlds.Length - 1) return;
			world++;
			UpdateUI();
			UpdateLevels();
		}

		public void PreviousWorld()
		{
			if (world <= 0) return;
			world--;
			UpdateUI();
			UpdateLevels();
		}
	}
}