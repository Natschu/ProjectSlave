using UnityEngine;


[RequireComponent(typeof(ConeRaycast))]
public class PatrolSpotlight : MonoBehaviour {

	private ConeRaycast raycast;
	private Light spotLight;

	void Start()
	{
		raycast = GetComponent<ConeRaycast>();
		spotLight = GetComponentInChildren<Light>();

		spotLight.range = raycast.range;
		spotLight.spotAngle = raycast.angle;
	}

	void Update()
	{
		spotLight.range = raycast.range;
		spotLight.spotAngle = raycast.angle;
	}
}
