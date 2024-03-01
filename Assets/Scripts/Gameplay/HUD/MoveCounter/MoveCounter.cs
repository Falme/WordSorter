using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class MoveCounter : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI counterText;

		private int counter = 0;

		public void Add()
		{
			counterText.text = (++counter).ToString();
		}

		public void Reset()
		{
			counter = 0;
			counterText.text = counter.ToString();
		}
	}
}