using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : Interactable {

	public Interaction interaction;
	public bool oneTimeUse = false;
	public bool useKey = false;

	private bool disableInteraction = false;
	private bool keyWasUsed = false;

	public override void Interact(){

		if(isUsed){
			return;
		}
		base.Interact();
		
		//Debug.Log("[ButtonInteractable] Interacting with " + transform.name);

		//Disable Interaction if this is one-time only
		if(!disableInteraction){

			//If key is not needed or was already used
			if(!useKey || keyWasUsed){
				interaction.Action();
				
				if(oneTimeUse){
					disableInteraction = true;
				}
			}
			//if key is needed
			else{
				if(PlayerController.UseKey()){
					interaction.Action();
					keyWasUsed = true;
					if(oneTimeUse){
						disableInteraction = true;
					}
				}
			}
		}			
	}
}
