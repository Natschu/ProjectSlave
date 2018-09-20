using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _5FlipGravity : MonoBehaviour{

	public Vector3 flipGravity;
	public  static bool isFlipped = false;

	private static Vector3 flippedGravity;
	private static Vector3 standardGravity = Physics.gravity;

	void Awake()
	{
		flippedGravity = flipGravity;
	}

	public static void Flip(){
		if(!isFlipped){
			Physics.gravity = flippedGravity;
			isFlipped = true;
		}
		else{
			Physics.gravity = standardGravity;
			isFlipped = false;
		}
	}
}
