using UnityEngine;

public class RestartButton : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;

	public void RestartLevel()
	{
		Popup.Instance.OpenPopup("Restart the level from the beginning?", PopupType.YES_NO, () =>
		{
			gameManager.Restart();
		});
	}
}
