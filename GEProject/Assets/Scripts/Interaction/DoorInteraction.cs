using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : Interaction {

	public float moveDoorTimer = 2f;
	public Vector3 moveDoorVector;

	//isActive = true => open
	//isActive = false => closed

	protected Vector3 originalPosition;
	protected IEnumerator coroutine;
	
	void Start()
	{
		originalPosition = transform.position;
		if(isActive){
			transform.position = originalPosition + moveDoorVector;
		}
	}

	public override void OnActivation(){
		base.OnActivation();
		//Debug.Log("DoorInteraction: OnActivation");

		if(coroutine != null){
			StopCoroutine(coroutine);	
		}
		
		coroutine = OpenDoor();
		StartCoroutine(coroutine);
	}
	public override void OnDeactivation(){
		base.OnDeactivation();
		//Debug.Log("DoorInteraction: OnDeactivation");

		if(coroutine != null){
			StopCoroutine(coroutine);	
		}

		coroutine = CloseDoor();
		StartCoroutine(coroutine);
	}

	protected IEnumerator OpenDoor(){
		Vector3 startPosition = transform.position;
		float startTime = Time.time;

		while(Time.time <= startTime + moveDoorTimer){
			float currentTime = UtilityFunctions.Remap(Time.time,
			startTime, startTime + moveDoorTimer, 0f, 1f);

			transform.position = Vector3.Lerp(startPosition,
									originalPosition + moveDoorVector,
									currentTime);
			yield return null;
		}
		transform.position = originalPosition + moveDoorVector;
	}

	protected IEnumerator CloseDoor(){
		Vector3 startVector = transform.position;
		float startTime = Time.time;

		while(Time.time <= startTime + moveDoorTimer){
			float currentTime = UtilityFunctions.Remap(Time.time,
			startTime, startTime + moveDoorTimer, 0f, 1f);

			transform.position = Vector3.Lerp(startVector,
									originalPosition,
									currentTime);
			yield return null;
		}
		transform.position = originalPosition;
	}
}
