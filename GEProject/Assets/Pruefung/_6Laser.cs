using UnityEngine;

public class _6Laser : MonoBehaviour {

	public GameObject receiver;
	public float randomStartTimeMax = 5f;
	public float timeActivePeriod = 2f;
	public LayerMask targetLayer;

	private LineRenderer laser;

	private bool laserIsActive;	
	private float timeNextLaser;
	private float timeStopLaser;	
	
	void Start()
	{
		laser = GetComponent<LineRenderer>();

		SetupLaser();
	}

	void SetupLaser(){
		laserIsActive = false;
		laser.enabled = false;

		timeNextLaser = Time.time + Random.Range(0f, randomStartTimeMax);
	}

	void Update(){
		if(Time.time > timeNextLaser && !laserIsActive){
			laserIsActive = true;
			laser.enabled = true;
			timeStopLaser = Time.time + timeActivePeriod;
		}

		if(laserIsActive){
			Vector3 posOffset = 
					new Vector3(0f, Random.value - 0.5f, 0f);
			laser.SetPosition(0, transform.position + posOffset);

			posOffset = new Vector3(0f, Random.value - 0.5f, 0f);
			laser.SetPosition(1, receiver.transform.position 
								+ posOffset);

			Vector3 direction = receiver.transform.position 
								- this.transform.position;
			
			Ray ray = new Ray(transform.position, direction.normalized);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, direction.magnitude, targetLayer)){
				Debug.Log("Disintegrated!");
			}
			if(Time.time > timeStopLaser){
				SetupLaser();
			}
		}
	}	
}
