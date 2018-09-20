using UnityEngine;

public class _10MoreInput : MonoBehaviour {
	void Update () {
		if(Input.GetMouseButtonDown(0)){}	//Left Mouse
		if(Input.GetMouseButton(1)){}		//Right Mouse
		if(Input.GetMouseButtonUp(2)){}		//Middle Mouse

		if(Input.GetKeyDown(KeyCode.Q)){}	//Keyboard: Q
		if(Input.GetKey(KeyCode.Q)){}
		if(Input.GetKeyUp(KeyCode.Q)){}

		//XBox Controller
		if(Input.GetKey(KeyCode.JoystickButton0)){}	//Any Controller: A
		if(Input.GetKey(KeyCode.Joystick1Button0)){} //1. Controller: A
		if(Input.GetKey(KeyCode.JoystickButton1)){} //B
		if(Input.GetKey(KeyCode.JoystickButton2)){} //X
		if(Input.GetKey(KeyCode.JoystickButton3)){} //Y
		if(Input.GetKey(KeyCode.JoystickButton4)){} //LB
		if(Input.GetKey(KeyCode.JoystickButton5)){} //RB
		if(Input.GetKey(KeyCode.JoystickButton6)){} //Back
		if(Input.GetKey(KeyCode.JoystickButton7)){} //Start
		if(Input.GetKey(KeyCode.JoystickButton8)){} //Left Stick Click
		if(Input.GetKey(KeyCode.JoystickButton9)){} //Right Stick Click

		float inputvalue = 0f;
		inputvalue = Input.GetAxis("XBox_LeftXAxes");
		/*More Axis:	- XBox_LeftYAxes
						- XBox_RightXAxes
						- XBox_RightYAxes
						- XBox_D-PadXAxes
						- XBox_D-PadYAxes
						- XBox_Triggers
						- XBox_LeftTrigger
						- XBox_RightTrigger
		*/
	}
}
