using UnityEngine;

namespace WordSorter
{
	public class LevelSelectButton : MonoBehaviour
	{
		private const string BodyText = "gameplay_popup_body_levelselect";

		public void ReturnToLevelSelection()
		{
			Popup.Instance.OpenPopup(
				BodyText,
				PopupType.YES_NO,
				() => LevelManager.Instance.ToLevelSelect()
			);
		}
	}
}