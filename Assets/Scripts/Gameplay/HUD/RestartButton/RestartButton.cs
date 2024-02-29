using UnityEngine;

namespace WordSorter
{
	public class RestartButton : MonoBehaviour
	{
		private const string BodyText = "gameplay_popup_body_restart";
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
}