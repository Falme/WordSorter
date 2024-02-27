using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private LevelButtonSettings levelButtonSettings;
	[SerializeField] private Image levelImage;

	[SerializeField] private bool initialState;

	public void Start()
	{
		ChangeNumber(transform.GetSiblingIndex() + 1);
		ChangeStatus(initialState);
	}

	public void ChangeNumber(int levelNumber)
	{
		levelText.text = levelNumber.ToString();
	}

	public void ChangeStatus(bool enable)
	{
		levelImage.sprite = enable ? levelButtonSettings.spriteEnabled : levelButtonSettings.spriteDisabled;
		levelImage.color = enable ? levelButtonSettings.colorEnabled : levelButtonSettings.colorDisabled;

		ShowText(enable);
	}

	public void ShowText(bool enable)
	{
		levelText.enabled = enable;
	}

	public void GoToLevel(string levelName)
	{
		LevelManager.Instance.LoadScene(levelName);
	}
}
