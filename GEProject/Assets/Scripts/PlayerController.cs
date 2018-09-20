using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Raycast")]
	public Camera cam;
	public Transform camTarget;
	public float range;
	public LayerMask layerMask;
	public bool enableDebugMode = false;

	[Space]
	[Header("Configuration")]
	public float walkSpeed = 5f;
	public float rotationSpeed = 90f;
	public float animDampTime = 0.1f;
	
	[Space]
	[Header("Ground Shader")]
	public Material circleShader;

	#region Key
	static int keyCount = 0;

	public static int KeyCount{
		get{ return keyCount; }
	}
	public static bool UseKey(){
		if(keyCount <= 0){
			return false;
		}
		
		keyCount--;
		return true;
	}
	public static void AddKey(){
		keyCount++;
	}
	#endregion

	Animator animator;
	Vector3 camRotation;

	float xMovement = 0f;
	float zMovement = 0f;

	ThirdPersonRaycast raycast;

	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		raycast = new ThirdPersonRaycast{
			StartPointPlayer = this.transform,
			StartPointCamera = cam,
			ReferencePointCamera = camTarget,
			Range = range,
			LayerMask = layerMask,
			EnableDebugMode = enableDebugMode
		};
	}

	void Update()
	{
		circleShader.SetVector("_Position", transform.position);

		MoveCharacter();
		AnimateCharacter();

		if(Input.GetKeyDown(KeyCode.E)){
			UseInteractable();
		}		
	}


	private void MoveCharacter()
	{
		if (!GameManager.gameIsPaused)
		{
			xMovement = Input.GetAxis("Horizontal");
			zMovement = Input.GetAxis("Vertical");
		}	

		camRotation = cam.transform.rotation.eulerAngles;
		camRotation.x = 0f;

		this.transform.eulerAngles = camRotation;
		this.transform.Translate(xMovement * Time.deltaTime * walkSpeed, 
								 0f, 
								 zMovement * Time.deltaTime * walkSpeed);

		}

	private void AnimateCharacter(){
		float speedPercent = (Mathf.Abs(xMovement) >= Mathf.Abs(zMovement)) ?
							  Mathf.Abs(xMovement) : Mathf.Abs(zMovement);

		animator.SetFloat("speedPercent", speedPercent, animDampTime, Time.deltaTime);
	}

	private void UseInteractable(){
		RaycastHit hit;
		if(raycast.ShootRay(out hit)){
			
			Interactable interactable = hit.collider.GetComponent<Interactable>();

			if(interactable != null){
				interactable.Interact();
			}
		}
	}
}
