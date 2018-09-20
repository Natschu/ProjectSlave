using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour {

	public GameObject image;

	private bool isActive = false;

	void Start(){
		image.SetActive(false);
	}

	void Update()
	{
		if(!isActive && PlayerController.KeyCount > 0){
			image.SetActive(true);
			isActive = true;
		}
		else if (PlayerController.KeyCount <= 0){
			image.SetActive(false);
			isActive = false;
		}
	}
}
