using UnityEngine;

public class ButtonInteractables : Interactable {

	public Interaction[] interactions;

	public override void Interact(){
		if(isUsed){
			return;
		}
		base.Interact();

		foreach(Interaction interaction in interactions){
			interaction.Action();
		}
	}
}
