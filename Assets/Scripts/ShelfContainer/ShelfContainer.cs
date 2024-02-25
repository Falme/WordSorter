using UnityEngine;

public class ShelfContainer : MonoBehaviour
{
	[SerializeField] private GameObject shelfPrefab;
	[SerializeField] private ShelfSettings shelfSettings;
	[SerializeField] private LevelConfiguration levelConfiguration;

	private void Start() => InstantiateShelves();

	private void InstantiateShelves()
	{
		for (int i = 0; i < levelConfiguration.shelvesData.Length; i++)
		{
			var _shelf = Instantiate(shelfPrefab, transform).GetComponent<Shelf>();
			_shelf.transform.localPosition = new Vector3(0f, i * -shelfSettings.verticalSpacing);

			_shelf.Initialize(shelfSettings, levelConfiguration, i);
		}
	}
}
