using UnityEngine;

public class Interactable : MonoBehaviour {

	public float waitForNextUse = 0.25f;

	[HideInInspector] public bool isUsed;
	private float timeBegin;
	private float timeCount;

	void Update()
	{
		if(isUsed){
			timeCount += Time.deltaTime;

			if(timeCount >= timeBegin + waitForNextUse){
				isUsed = false;
			}
		}
	}

	public virtual void Interact(){
		if(isUsed){
			return;
		}

		isUsed = true;
		timeBegin = Time.time;
		timeCount = timeBegin;

		//Debug.Log("[Interactable] Interacting with " + transform.name);
	}
}
