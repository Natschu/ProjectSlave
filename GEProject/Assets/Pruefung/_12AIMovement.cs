using UnityEngine;
using UnityEngine.AI;

public class _12AIMovement : MonoBehaviour {

	public float vision = 60f;

	private Vector3 target;
	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		target = transform.position;
	}

	void Update () {
		if(Input.GetMouseButton(0)){
			RaycastHit hit;
			Ray ray = Camera.main
				.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit)){
				target = hit.point;
			}
			Debug.Log(target.ToString());
		}

		agent.SetDestination(target);
		float distance = Vector3.Distance(transform.position, target);

		if(distance < agent.stoppingDistance){
			agent.isStopped = true;
		}
		else if(distance < vision){
			agent.isStopped = false;
			NavMeshPath path = agent.path;
			for(int i = 1; i < path.corners.Length; i++){
				Debug.DrawLine(path.corners[i-1], path.corners[i], 
				Color.red);
			}		
		}
	}
}
