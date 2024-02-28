using UnityEngine;

public class LevelSelectButton : MonoBehaviour
{
	private const string SceneName = "LevelSelect";

	public void ReturnToLevelSelection()
	{
		LevelManager.Instance.LoadScene(SceneName);
	}
}
