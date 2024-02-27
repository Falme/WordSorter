using UnityEngine;

[CreateAssetMenu(fileName = "LevelButtonSettings", menuName = "ScriptableObject/LevelButtonSettings")]
public class LevelButtonSettings : ScriptableObject
{
	public Sprite spriteEnabled, spriteDisabled;
	public Color colorEnabled, colorDisabled;
}