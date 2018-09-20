using UnityEngine;

public class _4Coin : MonoBehaviour {

	public float rotationSpeed = 10f;

	void Update()
	{
		transform.rotation *= Quaternion.Euler
			(Time.deltaTime * rotationSpeed, 0f,0f);
	}
	void OnTriggerEnter(Collider other)
	{
		//Collect points
		//_5FlipGravity.Flip();
		Destroy(this.gameObject);
	}
}
