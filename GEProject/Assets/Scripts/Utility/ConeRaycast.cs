using UnityEngine;

public class ConeRaycast : MonoBehaviour{

	#region Variables
	public Transform startPoint;
	public float range = 5;
	public float angle = 60f;
	public LayerMask layer;
	public int numOfRays = 7;
	public bool enableDebugMode;
	#endregion

	//Tracker are used for the Number of Rays and Angle Variables,
	//because there are values that dependent on those.

	private ChangeTracker<int> numOfRaysTracker;
	private Ray[] rays;
	private RaycastHit[] raycastHits;
	private RaycastHit latestHit;

	private ChangeTracker<float> angleTracker;
	private float angleMin;
	private float angleMax;
	private float deltaAngle;

	void Awake()
	{
		numOfRaysTracker = new ChangeTracker<int>(numOfRays);
		angleTracker = new ChangeTracker<float>(angle);
	}

	void Update()
	{
		//Change Tracker: Number of Rays
		if(!numOfRaysTracker.value.Equals(numOfRays))
		{
			if (enableDebugMode) { Debug.Log("ray update"); }
			numOfRaysTracker.value = numOfRays;
		}

		//Change Tracker: Angle
		if (!angleTracker.value.Equals(angle))
		{
			if (enableDebugMode) { Debug.Log("angle update"); }
			angleTracker.value = angle;
		}
	}

	#region Private Functions
	private void SetupAngles()
	{
		if (angleTracker.hasChanged)
		{
			//rotate angle by half
			angleMin = angle / -2f;
			angleMax = angle / 2f;

			//find distance between each angle
			deltaAngle = angle / (numOfRays - 1);

			//notify shader

			angleTracker.ResetChangedFlag();

			#region Debug
			if (enableDebugMode)
			{
				Debug.Log("Angle [" + angleMin + " - " + angleMax + "] => delta: " + deltaAngle);
			}
			#endregion
		}
	}
	
	private void SetupRays()
	{
		if (numOfRaysTracker.hasChanged)
		{
			rays = new Ray[numOfRays];
			raycastHits = new RaycastHit[numOfRays];

			deltaAngle = angle / (numOfRays - 1);

			numOfRaysTracker.ResetChangedFlag();

			#region Debug
			if (enableDebugMode) {
				Debug.Log("Rays Updated => " + numOfRays);
			}
			#endregion
		}
	}

	private void CalculateRays()
	{
		float currentAngle = (angleMin / 180) * Mathf.PI;
		for(int i = 0; i < numOfRays; i++)
		{
			#region Debug
			if (enableDebugMode) {
				Debug.Log("Iteration: " + i + " with current Angle: " + currentAngle + 
						  " => " + currentAngle / Mathf.PI * 180);
			}
			#endregion

			Vector3 direction = new Vector3(Mathf.Sin(currentAngle), 0f, Mathf.Cos(currentAngle));
			direction = transform.TransformDirection(direction);
			rays[i] = new Ray(transform.position, direction);

			#region Debug
			if (enableDebugMode) {
				Debug.DrawRay(rays[i].origin, rays[i].direction * range, Color.yellow);
			}
			#endregion

			currentAngle = (angleMin + (i + 1) * deltaAngle) / 180 * Mathf.PI;
		}
	}

	private bool ExecuteRaycasts()
	{
		bool hasHit = false;
		for(int i=0; i < numOfRays; i++)
		{
			//raycastHits[i] = new RaycastHit();
			if(Physics.Raycast(rays[i], out raycastHits[i], range, layer.value))
			{
				latestHit = raycastHits[i];
				hasHit = true;
			}
		}

		return hasHit;
	}
	#endregion

	void OnDrawGizmosSelected()
	{
		//Angle Min
		float angleMin = angle / -2f / 180 * Mathf.PI;
		Vector3 direction = new Vector3(Mathf.Sin(angleMin), 0f, Mathf.Cos(angleMin));
		direction = transform.TransformDirection(direction);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, direction * range);


		//Angle Max
		float angleMax = angle / 2f / 180 * Mathf.PI;
		direction = new Vector3(Mathf.Sin(angleMax), 0f, Mathf.Cos(angleMax));
		direction = transform.TransformDirection(direction);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, direction * range);


		//Range
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}

	public bool Cast()
	{
		SetupAngles();
		SetupRays();
		CalculateRays();

		return ExecuteRaycasts();
	}

	//DOESNT WORK (raycasthits has 'empty' hits)
	public void GetInformation(out RaycastHit[] raycastHits)
	{
		raycastHits = this.raycastHits;
	}
	//public RaycastHit[] GetInformation()
	//{
	//	return raycastHits;
	//}
	public void GetInformation(out RaycastHit raycastHit)
	{
		raycastHit = latestHit;
	}
	public RaycastHit GetInformation()
	{
		return latestHit;
	}
}
