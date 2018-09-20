using UnityEngine;

public class _7Input : MonoBehaviour {
	public float rollSpeed = 12f;
	public string speedString = "Vertical";
	public float rotSpeed = 180f;
	public string rotString = "Horizontal";

	private Rigidbody rb;
	private float speedInput;
	private float rotInput;
	
	void Awake(){
		rb = GetComponent<Rigidbody>();
	}
	void Update(){
		speedInput = Input.GetAxis(speedString);
		rotInput = Input.GetAxis(rotString);
	}
	void FixedUpdate(){
		Vector3 direction = transform.forward * speedInput 
							* rollSpeed * Time.deltaTime;
		rb.MovePosition(rb.position + direction);

		float rotation = rotInput * rotSpeed * Time.deltaTime;
		rb.MoveRotation(rb.rotation 
						* Quaternion.Euler(0f, rotation, 0f));
	}
}
