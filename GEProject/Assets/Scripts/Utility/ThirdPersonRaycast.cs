using UnityEngine;

/// <summary>
/// Feed information into this class to create an simple ThirdPerson Raycast.
/// </summary>
public class ThirdPersonRaycast {

	#region Variables
	private Transform player;
	private Camera cam;
	private Transform cameraTarget;
	private float range;
	private int layerMask;

	private bool enableDebugMode;
	#endregion

	#region Properties
	public Transform StartPointPlayer
	{
		get	{ return player; }
		set	{ player = value; }
	}

	public Camera StartPointCamera
	{
		get { return cam; }
		set { cam = value; }
	}

	public Transform ReferencePointCamera
	{
		get { return cameraTarget; }
		set { cameraTarget = value; }
	}

	public float Range
	{
		get { return range; }
		set	{ range = value; }
	}

	public int LayerMask
	{
		get { return layerMask; }
		set { layerMask = value; }
	}

	public bool EnableDebugMode
	{
		get { return enableDebugMode; }
		set { enableDebugMode = value; }
	}
	#endregion

	public ThirdPersonRaycast()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		cam = Camera.main;
		cameraTarget = player;
		range = Mathf.Infinity;
		enableDebugMode = false;
		layerMask = Physics.DefaultRaycastLayers;
	}

	public bool ShootRay(out RaycastHit hit)
	{
		//Set hit to empy RaycastHit
		//if somehting has been hit, hit will be playerHit
		hit = new RaycastHit();

		//Setup Camera Raycast
		RaycastHit cameraHit;
		Ray cameraRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

		Target target = new Target();

		//Debug
		if (enableDebugMode)
		{
			Debug.DrawRay(cameraRay.origin, 
						cameraRay.direction * (range + Vector3.Distance(
							cam.transform.position, cameraTarget.position)), 
						Color.blue);
		}

		//if camera raycast is successfull
		if (Physics.Raycast(cameraRay, out cameraHit, 
							range + Vector3.Distance(cam.transform.position, 
													cameraTarget.position), 
							layerMask))
		{
			//Setup Player Raycast
			RaycastHit playerHit;
			Ray playerRay = new Ray(player.transform.position,
									cameraHit.point - player.transform.position);

			//Debug
			if (enableDebugMode)
			{
				Debug.DrawRay(cameraRay.origin, cameraRay.direction * range, Color.blue);
			}

			//if player ray is successfull
			if (Physics.Raycast(playerRay, out playerHit, range, layerMask))
			{
				hit = playerHit;
				return true;
			}
		}
		return false;
	}

}