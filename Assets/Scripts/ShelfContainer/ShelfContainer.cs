using UnityEngine;

public class ShelfContainer : MonoBehaviour
{
	[SerializeField] private GameObject shelfPrefab;
	[SerializeField] private ShelfSettings shelfSettings;
	[SerializeField] private LevelConfiguration levelConfiguration;

	private void Start() => InstantiateShelves();

	private void InstantiateShelves()
	{
		for (int a = 0; a < levelConfiguration.shelvesData.Length; a++)
		{
			var _shelf = Instantiate(shelfPrefab, transform).GetComponent<Shelf>();
			_shelf.transform.localPosition = new Vector3(0f, a * -shelfSettings.shelfVerticalSpacing);

			_shelf.Initialize(shelfSettings, levelConfiguration, a);
		}
	}
}
