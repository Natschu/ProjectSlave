using UnityEngine;

//public enum PatrolTurnDirection { TurnLeft, TurnRight}

[System.Serializable]
public class PatrolCheckpoint{

	[Header("Main")]
	public Transform transform;
	public bool isPassthrough = true;

	[Header("Passthrough Information")]
	[Tooltip("This has to be higher then NavMeshAgents stoping distance")]
	public float smoothCurveRadius = 0.1f;

	[Header("Wait Information")]
	public float waitDuration = 0f;
	public float yRotation = 0f;

	//[Tooltip("Turn left or right from specified yRotation")]
	//public PatrolTurnDirection patrolTurnDirection = PatrolTurnDirection.TurnLeft;
}

