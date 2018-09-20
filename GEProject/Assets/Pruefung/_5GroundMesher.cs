using UnityEngine;

public class _5GroundMesher : MonoBehaviour {

	public Material material;
	public float radius = 0.5f;
	public float radiusOffSet = 3.0f;

	private GameObject spherePrefab;
	private int panelIndex = 0;

	void Start () {
		spherePrefab = GameObject.Find("spherePrefab");
	}

	void Update () {
		Vector3 newPos = spherePrefab.transform.position 
			+ spherePrefab.GetComponent<Rigidbody>().velocity.normalized * radius;
		newPos.y = 0f;

		AddPanel(newPos);
		RemovePanel(spherePrefab.transform.position);
	}

	private void AddPanel(Vector3 newPos){
		GameObject floorPanel = new GameObject("GroundPanel");
		MeshFilter meshFilter = floorPanel.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = floorPanel.AddComponent<MeshRenderer>();
		MeshCollider meshCollider = floorPanel.AddComponent<MeshCollider>();

		float sizeOfMesh = 2.0f;

		meshFilter.mesh.vertices = new Vector3[]{
			new Vector3(-sizeOfMesh, 0, -sizeOfMesh), //vorne links
			new Vector3(sizeOfMesh, 0, sizeOfMesh), //rechts hinten
			new Vector3(sizeOfMesh, 0, -sizeOfMesh), //rechts vorne
			new Vector3(-sizeOfMesh, 0, sizeOfMesh) //links hinten
		};
		meshFilter.mesh.triangles = new int[]{0, 1, 2, 0, 3 ,1};
		meshFilter.mesh.uv = new Vector2[]{
			new Vector2(0, 0),
			new Vector2(1, 1),
			new Vector2(1, 0),
			new Vector2(0, 1)
		};

		floorPanel.transform.position = newPos;
		
		meshRenderer.material = material;
		meshCollider.sharedMesh = meshFilter.mesh;

		floorPanel.tag = "FloorPanel";
		floorPanel.transform.parent = this.transform;
		panelIndex++;
	}

	private void RemovePanel(Vector3 currentPos){
		foreach(GameObject panel in GameObject.FindGameObjectsWithTag("FloorPanel")){
			float distance = Vector3.Distance(currentPos, panel.transform.position);
			if(distance > radius + radiusOffSet){
				panel.AddComponent<Rigidbody>();
				Destroy(panel, 2f);
			}
		}
	}
}
