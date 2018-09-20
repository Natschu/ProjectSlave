using UnityEngine;
using UnityEngine.Audio;

public class _13AudioSnapShotManager : MonoBehaviour {

	public AudioMixerSnapshot snapshotPlayMode;
	public AudioMixerSnapshot snapshotPauseMode;
	
	private bool pause = false;

	void Update(){
		
		if(Input.GetKeyDown(KeyCode.P)){
			pause = !pause;
			if(pause == true){
				snapshotPauseMode.TransitionTo(1.5f);
			}
			else{
				snapshotPlayMode.TransitionTo(1.5f);
			}
		}
	}
}
