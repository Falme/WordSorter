using UnityEngine;

namespace WordSorter
{
	public class LevelSelectButton : MonoBehaviour
	{
		private const string SceneName = "LevelSelect";
		private const string BodyText = "Return to Level Selection?";

		public void ReturnToLevelSelection()
		{
			Popup.Instance.OpenPopup(
				BodyText,
				PopupType.YES_NO,
				() => LevelManager.Instance.LoadScene(SceneName)
			);
		}
	}
}