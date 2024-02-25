using UnityEngine;

[CreateAssetMenu(fileName = "ShelfSettings", menuName = "ScriptableObject/ShelfSettings")]
public class ShelfSettings : ScriptableObject
{
	public float letterSpacing;
	public float shelfVerticalSpacing;
	public float shelfWidthIncrement;
}