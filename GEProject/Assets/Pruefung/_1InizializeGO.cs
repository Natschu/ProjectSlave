using UnityEngine;

public class _1InizializeGO : MonoBehaviour {

	public GameObject prefab;
	public Transform spawnPoint;
	public float lifetime;
	
	private GameObject currentPrefab;
	private float timePassed = 0f;

	void Awake()
	{
		currentPrefab = Instantiate(prefab, spawnPoint);
	}

	void Update()
	{
		timePassed += Time.deltaTime;
		
		if(timePassed >= lifetime){
			Destroy(currentPrefab);

			timePassed = 0f;
			currentPrefab = Instantiate(prefab, spawnPoint);
		}
	}
}
