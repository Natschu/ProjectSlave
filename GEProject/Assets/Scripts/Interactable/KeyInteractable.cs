using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable {

	public override void Interact(){
		// if(isUsed){
		// 	return;
		// }
		// base.Interact();

		PlayerController.AddKey();
		Destroy(gameObject);
		
	}
}
