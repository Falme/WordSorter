using UnityEngine;

public class RestartButton : MonoBehaviour
{
	private const string BodyText = "Restart the level from the beginning?";
	[SerializeField] private GameManager gameManager;

	public void RestartLevel()
	{
		Popup.Instance.OpenPopup(
			BodyText,
			PopupType.YES_NO,
			gameManager.Restart
			);
	}
}
