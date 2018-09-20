using UnityEngine;

public class _9MouseInputAndReset : MonoBehaviour {

	private GameObject currentGrab;

	void FixedUpdate(){
		if(Input.GetMouseButton(0)){
			RaycastHit hit;
			Ray ray = Camera.main
				.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit)){
				if(hit.transform.CompareTag("MoveableWithMouse")){
					Debug.Log("Hit Moveable!");
					currentGrab = hit.transform.gameObject;
					currentGrab.GetComponent<Collider>()
						.enabled = false;
					currentGrab.GetComponent<Rigidbody>()
						.isKinematic = true;
				}
			}
			if(currentGrab != null){
				currentGrab.GetComponent<Rigidbody>()
					.MovePosition(hit.point + Vector3.up * 2f);
			}
		}
		
		if(Input.GetMouseButtonUp(0)){
			if(currentGrab != null){
				currentGrab.GetComponent<Collider>()
					.enabled = true;
				currentGrab.GetComponent<Rigidbody>()
					.isKinematic = false;
			}
			currentGrab = null;
		}
	}
}
