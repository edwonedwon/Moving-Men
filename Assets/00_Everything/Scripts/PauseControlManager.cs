using UnityEngine;
using System.Collections;
using InControl;

// THIS SCRIPT ONLY MANAGES THE TURNING ON AND OFF OF THE PAUSE MENU
// FOR THE PAUSE MENU DETAILED CONTROLS SEE PAUSE MENU MANAGER

public class PauseControlManager : MonoBehaviour {

	private bool pauseEnabled;
	private bool canPressPause;
	
	InputDevice inputDevice;

	void Start () 
	{
		pauseEnabled = false;
		canPressPause = true;
		Time.timeScale = 1;
	}
	
	void Update () 
	{
//		Debug.Log("pauseEnabled = " + pauseEnabled);
//		Debug.Log("canPressPause = " + canPressPause);

		InputManager.Update();
		inputDevice = InputManager.ActiveDevice;

		if(inputDevice.DPadDown)
		{
//			Debug.Log ("dpad down");
			if(canPressPause == true && pauseEnabled == true)
			{
				// end pause
				pauseEnabled = false;
				canPressPause = false;
				gameObject.GetComponent<TweenAlpha>().PlayReverse();
				Time.timeScale = 1;
			} else if (canPressPause == true && pauseEnabled == false)
			{
				// begin pause
				pauseEnabled = true;
				canPressPause = false;
				gameObject.GetComponent<TweenAlpha>().PlayForward();
				Time.timeScale = 0;
			}
		}

		if(inputDevice.DPadDown == false)
		{
//			Debug.Log ("dpad up");
			canPressPause = true;
		}
	}
}
