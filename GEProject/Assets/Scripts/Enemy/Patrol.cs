using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ConeRaycast))]
public class Patrol : MonoBehaviour {

	public PatrolCheckpoint[] patrolCheckpoints;
	private int current = 0;

	private NavMeshAgent agent;
	private ConeRaycast raycast;

	//Control Variables
	private bool updateCheckpoint = true;
	private bool destinationReached = false;

	//Wait Variables
	private float waitStopwatch = 0f;
	private float waitDuration = 0f;
	private Quaternion yRotation;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		raycast = GetComponent<ConeRaycast>();

		if (patrolCheckpoints.Length == 0)
		{
			Debug.LogWarning(gameObject.name + ": Patrol Checkpoints List is empty!");
			return;
		}		
	}
	
	void Update () {
		EvaluateVision();

		if (patrolCheckpoints.Length == 0)
			return;

		//Rotate();
		if (updateCheckpoint)
		{
			SetCheckpointData();
		}

		if (!destinationReached)
		{
			CheckDestinationReached();
		}

		if (destinationReached)
		{
			WaitAtCheckpoint();
		}
	}


	private void SetCheckpointData()
	{
		//Set Wait Information
		if (!(patrolCheckpoints[current].isPassthrough))
		{
			agent.autoBraking = true;
			waitStopwatch = 0f;
			waitDuration = patrolCheckpoints[current].waitDuration;
			yRotation = Quaternion.Euler(0f, patrolCheckpoints[current].yRotation, 0f);
		}
		else
		{
			agent.autoBraking = false;
			if(patrolCheckpoints[current].smoothCurveRadius <= agent.stoppingDistance)
			{
				patrolCheckpoints[current].smoothCurveRadius += 0.1f;
			}

			waitStopwatch = 0f;
			waitDuration = 0f;
		}

		updateCheckpoint = false;
		destinationReached = false;

		agent.destination = patrolCheckpoints[current].transform.position;
	}

	private void CheckDestinationReached()
	{
		if (!agent.pathPending)
		{
			if (!(patrolCheckpoints[current].isPassthrough))
			{
				if (agent.remainingDistance <= agent.stoppingDistance)
				{
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
					{
						destinationReached = true;
					}
				}
			}
			else 
			{
				if (agent.remainingDistance <= patrolCheckpoints[current].smoothCurveRadius)
				{
					destinationReached = true;
				}
			}
		}		
	}

	private void WaitAtCheckpoint()
	{
		agent.isStopped = true;

		
		if (!(patrolCheckpoints[current].isPassthrough))
		{
			agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, yRotation, waitStopwatch);
		}

		waitStopwatch += Time.deltaTime;
		//If waitDuration is 0, the if value is automaticly true
		if (patrolCheckpoints[current].isPassthrough || waitStopwatch >= waitDuration)
		{
			//set next checkpoint
			current++;
			if (current >= patrolCheckpoints.Length)
			{
				current = 0;
			}

			//TODO: IMPLEMENT ROTATION
			//Vector3 targetDir = patrolCheckpoints[current].transform.position - transform.position;
			//float angle = Vector3.Angle(targetDir, transform.forward);
			//Debug.Log(angle);

			updateCheckpoint = true;
			agent.isStopped = false;			
		}	
	}

	private void EvaluateVision()
	{
		if (raycast.Cast())
		{
			//If tag is important: create new if statement
			RaycastHit hit = raycast.GetInformation();

			if (hit.transform.gameObject.CompareTag("Player"))
			{
				GameManager.instance.GameOver();
			}
			//if(hit.transform.tag == "Player")
			//{
			//	Debug.Log("Found player!");
			//}

		}
	}
}
