using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCanvas : MonoBehaviour {

	public GameObject player;
	public float distanceCorrection = -0.1f;
	public GameObject canvas;
	public float minScale;
	public float maxScale;

	private bool inRange = false;
	private float range;
	private PlayerController playerController;
	private Interactable interactable;

	void Awake()
	{
		if(player == null){
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if(interactable == null){
			interactable = GetComponent<Interactable>();
		}
	}

	void Start()
	{
		playerController = player.GetComponent<PlayerController>();
		SphereCollider collider = GetComponent<SphereCollider>();
		range = playerController.range + collider.radius;

		//inRange = (Vector3.Distance(player.transform.position, this.transform.position) <= range) ? true : false;
	}

	void Update () {
		if(!interactable.isUsed){
			float distance = Vector3.Distance(player.transform.position, this.transform.position) - distanceCorrection;
			inRange = (distance <= range) ? true : false;
			
			if (inRange)
			{
				float canvasScale = Mathf.Lerp(minScale, maxScale, distance/range);
				canvas.SetActive(true);
				canvas.transform.localScale = new Vector3(canvasScale, canvasScale, canvasScale);
			}
			else
			{
				canvas.SetActive(false);
			}	
		}
		else{
			canvas.SetActive(false);
		}	
	}
}
