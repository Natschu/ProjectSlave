using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _11Cameras : MonoBehaviour {

	public GameObject player;
	public GameObject target;
	public GameObject cam;

	public Vector3 towerPose = new Vector3(0f, 15f, 0f);
	public Vector3 thirdPerson = new Vector3(-0.5f, 2f, -2f);
	public Vector3 egoPerson = new Vector3(0f, 1f, 0f);
	public Vector3 offsetPosOverview = new Vector3(0f, 15f, 0f);
	public Vector3 offsetRotOverview = new Vector3(90f, 180f, 0f);

	public float rotationSpeed = 180f;
	private float mouseRotation = 0f;

	private int numOfCameraSettings = 7;
	private int currentCamera = 0;

	void Update(){
		if(Input.GetKeyDown(KeyCode.K)){
			currentCamera++;
			currentCamera %= numOfCameraSettings;
		}
	}
	void LateUpdate(){
		cam.GetComponent<Camera>().orthographic = false;
		switch(currentCamera){
			case 0:
				cam.transform.position = towerPose;
				cam.transform.LookAt(player.transform.position);
				break;
			case 1:
				float yWinkel = player.transform.eulerAngles.y;
				Quaternion rotation = Quaternion
									.Euler(0f, yWinkel, 0f);
				
				cam.transform.position = player.transform.position 
										+ rotation * thirdPerson;
				cam.transform.LookAt(player.transform);
				break;
			case 2:
				cam.transform.position = player.transform.position 
										+ egoPerson;
				cam.transform.rotation = player.transform.rotation;
				break;
			case 3:
				Vector3 directionPlayerTarget 
						= target.transform.position
						- player.transform.position;
				float distance = directionPlayerTarget.magnitude;

				cam.transform.position = player.transform.position
				+ offsetPosOverview + directionPlayerTarget * 0.5f;
				cam.transform.rotation = player.transform.rotation
					* Quaternion.Euler(offsetRotOverview);
				cam.GetComponent<Camera>().orthographic = true;

				if(distance > 10f){
					cam.GetComponent<Camera>()
						.orthographicSize = distance;
				}
				break;
			case 4:
				cam.transform.position = player.transform.position 
										+ egoPerson;

				float horizontal = Input.GetAxis("Mouse X") 
									* rotationSpeed;

				mouseRotation += horizontal * rotationSpeed 
								* Time.deltaTime;
				cam.transform.rotation = Quaternion
									.Euler(0f, mouseRotation, 0f);
				break;
			case 5:
				cam.transform.position = player.transform.position 
										+ egoPerson;

				float mousePosX = (Input.mousePosition.x 
									/ Screen.width) - 0.5f;
				float mousePosY = (Input.mousePosition.y 
									/ Screen.height) - 0.5f;
				cam.transform.rotation = Quaternion
					.Euler(mousePosY * 45, mousePosX * 360, 0) 
					* player.transform.rotation;
				break;
			case 6:
				cam.transform.position = player.transform.position
										+ egoPerson;
				cam.transform.LookAt(player.transform);
				break;
		}
	}
}
