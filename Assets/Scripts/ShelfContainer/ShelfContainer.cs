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
			var shelf = Instantiate(shelfPrefab, transform).GetComponent<Shelf>();
			shelf.transform.localPosition = new Vector3(0f, i * -shelfSettings.verticalSpacing);

			shelf.Initialize(shelfSettings, levelConfiguration, i);
		}
	}
}
