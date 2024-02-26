using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfiguration", menuName = "ScriptableObject/LevelConfiguration")]
public class LevelConfiguration : ScriptableObject
{
	public int shelfCapacity;
	public WordData[] shelvesData;
}