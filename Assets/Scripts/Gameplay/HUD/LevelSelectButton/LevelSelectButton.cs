using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
	private const string SceneName = "LevelSelect";

	public void ReturnToLevelSelection()
	{
		Popup.Instance.OpenPopup("Return to Level Selection?", PopupType.YES_NO, () =>
		{
			LevelManager.Instance.LoadScene(SceneName);
		});
	}
}
