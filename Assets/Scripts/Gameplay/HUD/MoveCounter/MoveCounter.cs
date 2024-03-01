using TMPro;
using UnityEngine;

namespace WordSorter
{
	public class MoveCounter : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI counterText;

		public int Counter { get; private set; } = 0;

		private void OnEnable() => ShelfManager.SuccessfulMoveEvent += Add;
		private void OnDisable() => ShelfManager.SuccessfulMoveEvent -= Add;

		public void Add()
		{
			counterText.text = (++Counter).ToString();
		}

		public void Restart()
		{
			Counter = 0;
			counterText.text = Counter.ToString();
		}
	}
}