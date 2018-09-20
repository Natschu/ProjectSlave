using UnityEngine;
using UnityEngine.Audio;

public class _3Bumper : MonoBehaviour {

	public float force = 10f;

	private void OnCollisionEnter(Collision other)
	{
		Vector3 speed = -other.contacts[0].normal * force;
		speed.y = 0f;

		other.gameObject.GetComponent<Rigidbody>()
			.AddForce(speed, ForceMode.Impulse);
		
		AudioSource sound = GetComponent<AudioSource>();
		sound.Play();
	}
}
