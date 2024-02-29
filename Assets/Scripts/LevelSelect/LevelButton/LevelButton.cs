using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WordSorter
{
	public class LevelButton : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI levelText;
		[SerializeField] private LevelButtonSettings levelButtonSettings;
		[SerializeField] private Image levelImage;

		private Button button;

		public Level LevelData { get; set; }

		private void Awake()
		{
			button = GetComponent<Button>();

			ChangeNumber(transform.GetSiblingIndex() + 1);
		}

		public void ChangeNumber(int levelNumber)
		{
			levelText.text = levelNumber.ToString();
		}

		public void ChangeStatus(bool enable)
		{
			button.interactable = enable;

			levelImage.sprite = enable ? levelButtonSettings.spriteEnabled : levelButtonSettings.spriteDisabled;
			levelImage.color = enable ? levelButtonSettings.colorEnabled : levelButtonSettings.colorDisabled;

			ShowText(enable);
		}

		public void ShowText(bool enable)
		{
			levelText.enabled = enable;
		}

		public void GoToLevel()
		{
			LevelManager.Instance.CurrentLevel = LevelData;
			LevelManager.Instance.ToGameplay();
		}
	}
}