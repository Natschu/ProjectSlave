using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject lookAt;
	public Vector3 offset;

	[Header("Start Rotation")]
	public Vector2 startRotation = new Vector2(0f, 20f);
	[Space]
	
	[Header("Y-Axis Boundary")]
	public float YAxisMin = 0.0f;
	public float YAxisMax = 50.0f;

	[Header("Sensitivity")]
	public Vector2 sensitivity = new Vector2(4f, 1f);

	private Vector2 currentRotation;

	void Start()
	{
		currentRotation = startRotation;
	}

	void Update()
	{
		if (!GameManager.gameIsPaused)
		{
			currentRotation.x += Input.GetAxis("Mouse X") * sensitivity.x;
			currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity.y;
			//Debug.Log("horizontal: " + currentX + " | vertical: " + currentY);
		}

		currentRotation.y = Mathf.Clamp(currentRotation.y, YAxisMin, YAxisMax);
	}

	void LateUpdate()
	{
		Quaternion rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

		transform.position = lookAt.transform.position + (rotation * offset);
		transform.LookAt(lookAt.transform);
	}
}
