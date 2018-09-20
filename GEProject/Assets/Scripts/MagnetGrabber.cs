using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetGrabber : MonoBehaviour {

	[Header("References")]
	public Transform player;
	public Camera cam;
	public Transform camTarget;

	[Header("Properties")]
	public float range = 10f;
	public float pickUpRadius = 0.10f;
	public float smoothingStart = 1f;
	public float smoothingEnd = 5f;

	private int layerMask;

	private ThirdPersonRaycast raycast;
	private Target target;

	void Start () { 
		//Debug.Log(BitmaskHelper.ConvertBitmaskToString(layerMask));
		BitmaskHelper.GetBitmask(LayerMask.NameToLayer("Player"), out layerMask, true);
		raycast = new ThirdPersonRaycast
		{
			StartPointPlayer = player,
			StartPointCamera = cam,
			ReferencePointCamera = camTarget,
			Range = range,
			LayerMask = layerMask,
			EnableDebugMode = false
		};
	}
	
	void Update () {
		//Make to only interact by player input
		if (!GameManager.gameIsPaused)
		{
			if (FindMagnet())
			{
				if (Input.GetButton("Interact"))
				{
					StartCoroutine("GrabMagnet", target);
				}				
			}
		}
		
	}

	private bool FindMagnet()
	{
		// Target tempTarget = raycast.ShootRay();
		// if (tempTarget.Tag.Equals("Magnetic"))
		// {
		// 	target = tempTarget;
		// 	return true;
		// }
		return false;
	}

	IEnumerator GrabMagnet(Target target)
	{
		if (target.Object.GetComponent<Rigidbody>() != null)
		{
			target.Object.GetComponent<Rigidbody>().isKinematic = true;
			target.Object.GetComponent<Rigidbody>().detectCollisions = false;
		}

		Transform targetOrigin = target.Object.transform;

		while (Vector3.Distance(this.transform.position, targetOrigin.position) > pickUpRadius)
		{
			//Debug.Log(targetOrigin.ToString());
			float timeBegin = Time.time; 
			float smoothingCurrent = Mathf.Lerp(smoothingStart, smoothingEnd, 
												timeBegin - Time.time);

			target.Object.transform.position = Vector3.Lerp(targetOrigin.position, 
															this.transform.position, 
															Time.deltaTime * smoothingCurrent);
			yield return null;
		}

		if (target.Object.GetComponent<Rigidbody>() != null)
		{
			target.Object.GetComponent<Rigidbody>().isKinematic = false;
			target.Object.GetComponent<Rigidbody>().detectCollisions = true;
		}
	}
}

