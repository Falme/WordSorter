using UnityEngine;

[CreateAssetMenu(fileName = "BlockSettings", menuName = "ScriptableObject/BlockSettings")]
public class BlockSettings : ScriptableObject
{
	public Color defaultColor;
	public Color highlightColor;
}