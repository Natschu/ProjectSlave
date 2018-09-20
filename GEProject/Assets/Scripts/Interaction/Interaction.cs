using UnityEngine;

public class Interaction : MonoBehaviour {

	public bool isActive = false;
	
	public virtual void Action(){
		//Debug.Log("Interaction: Action");
		if(!isActive){
			OnActivation();
		}
		else{
			OnDeactivation();
		}
	}

	public virtual void OnActivation(){
		isActive = true;
		//Debug.Log("Interaction: OnActivation");
	}

	public virtual void OnDeactivation(){
		isActive = false;
		//Debug.Log("Interaction: OnDeactivation");
	}
}
