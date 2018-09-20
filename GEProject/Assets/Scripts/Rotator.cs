﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float speed = 5f;
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * speed);
	}
}