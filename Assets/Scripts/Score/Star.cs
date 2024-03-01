using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WordSorter
{
	public class Star : MonoBehaviour
	{
		[SerializeField] private ScoreSettings scoreSettings;
		[SerializeField] private float delayResult = 1f;

		private Image image;

		private void Awake()
		{
			image = GetComponent<Image>();
		}

		public void Show(bool enable)
		{
			if (enable) StartCoroutine(DelayShow());
		}

		private IEnumerator DelayShow()
		{
			yield return new WaitForSeconds(delayResult);
			image.color = scoreSettings.highlightColor;
		}

		public void Restart()
		{
			image.color = scoreSettings.defaultColor;
		}

	}
}
