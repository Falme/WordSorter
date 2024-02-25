using UnityEngine;

[CreateAssetMenu(fileName = "ShelfSettings", menuName = "ScriptableObject/ShelfSettings")]
public class ShelfSettings : ScriptableObject
{
	public float blockSpacing;
	public float verticalSpacing;
	public float widthIncrement;
}