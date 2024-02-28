using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSelectionConfiguration", menuName = "ScriptableObject/LevelSelectionConfiguration")]
public class LevelSelectionConfiguration : ScriptableObject
{
	public WorldSelectData[] worlds;
}

[Serializable]
public class WorldSelectData
{
	public string worldName;
	public LevelSelectData[] levels;
}

[Serializable]
public class LevelSelectData
{
	public LevelConfiguration levelData;
	public LevelConfiguration nextLevel;
	public bool isEnabled;
}