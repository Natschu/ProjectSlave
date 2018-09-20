using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInteractCanvas : MonoBehaviour {

	public Camera cam;


	void Start () {
		if(cam == null)
		{
			cam = Camera.main;
		}
		transform.rotation = Quaternion.Euler(0f, 
											cam.transform.rotation.eulerAngles.y,
											0f);

	}
	
	void Update () {
		transform.rotation = Quaternion.Euler(0f, 
											cam.transform.rotation.eulerAngles.y,
											0f);

		//Flip Canvas if Cam is behind Object
		Vector3 objectNormal = transform.rotation * Vector3.forward;
		Vector3 cameraToText = transform.position - cam.transform.position;
		float f = Vector3.Dot(objectNormal, cameraToText);
		if (f < 0f)
		{
			transform.Rotate(0f, 180f, 0f);
		}
	}
}
