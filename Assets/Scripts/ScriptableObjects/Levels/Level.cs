using UnityEngine;

namespace WordSorter
{
	[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObject/Level")]
	public class Level : ScriptableObject
	{
		public int index;
		public int shelfCapacity;
		public WordData[] shelvesData;
		public Level nextLevel;
		public int minimumMoves;
	}
}