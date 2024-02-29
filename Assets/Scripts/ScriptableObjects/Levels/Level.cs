using UnityEngine;

namespace WordSorter
{
	[CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObject/LevelConfiguration")]
	public class Level : ScriptableObject
	{
		public int shelfCapacity;
		public WordData[] shelvesData;
		public Level nextLevel;
	}
}