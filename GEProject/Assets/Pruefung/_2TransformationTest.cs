using UnityEngine;

public class _2TransformationTest : MonoBehaviour {

	public bool startWithTranslation = true;
	public Vector3 vectorTranslation;
	public Vector3 eulerAnglesRotation;

	private MeshFilter meshFilter;
	private Vector3[] originalVertices;
	private Vector3[] newVertices;

	private Matrix4x4 translationMatrix;
	private Quaternion rotation;
	Matrix4x4 rotationMatrix;

	void Start()
	{
		meshFilter = GetComponent<MeshFilter>();
		originalVertices = meshFilter.mesh.vertices;
		newVertices = new Vector3[originalVertices.Length];

		translationMatrix = Matrix4x4.Translate(vectorTranslation);

		rotation = Quaternion.Euler(eulerAnglesRotation.x,
											   eulerAnglesRotation.y,
											   eulerAnglesRotation.z);
		rotationMatrix = Matrix4x4.Rotate(rotation);

		if(startWithTranslation){
			StartTranslationWithMeshFilter();
			//StartTranslationWithoutMeshFilter();
		}
		else{
			StartRotationWithMeshFilter();
			//StartRotationWithoutMeshFilter();
		}
	}




	void StartTranslationWithMeshFilter(){
		Matrix4x4 transformationMatrix = 
					rotationMatrix * translationMatrix;

		//currentPrefab = Instantiate(prefab, spawnPoint);
		for(int i = 0; i<originalVertices.Length; i++){
			newVertices[i] = transformationMatrix.MultiplyPoint3x4
							(originalVertices[i]);
		}
		meshFilter.mesh.vertices = newVertices;
	}
	void StartRotationWithMeshFilter(){
		Matrix4x4 transformationMatrix = 
					translationMatrix * rotationMatrix;

		for(int i = 0; i<originalVertices.Length; i++){
			newVertices[i] = transformationMatrix.MultiplyPoint3x4
							(originalVertices[i]);
		}
		meshFilter.mesh.vertices = newVertices;
	}

	void StartTranslationWithoutMeshFilter(){
		this.transform.localPosition = vectorTranslation;
		this.transform.parent.transform.rotation = 
			Quaternion.Euler(eulerAnglesRotation);
	}
	void StartRotationWithoutMeshFilter(){
		this.transform.parent.transform.rotation =
			Quaternion.Euler(eulerAnglesRotation);
		this.transform.localPosition = vectorTranslation;
	}
}
