using UnityEngine;
using System.Collections;
using InControl;

// THIS SCRIPT ONLY MANAGES THE TURNING ON AND OFF OF THE PAUSE MENU
// FOR THE PAUSE MENU DETAILED CONTROLS SEE PAUSE MENU MANAGER

public class PauseControlManager : MonoBehaviour {

	private bool pauseEnabled;
	private bool canPressPause;
	
	InputDevice inputDevice;

	GameObject UIRoot;

	void Start () 
	{
		pauseEnabled = false;
		canPressPause = true;
		Time.timeScale = 1;
		UIRoot = transform.GetChild(0).gameObject;
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
				EndPause();
			} else if (canPressPause == true && pauseEnabled == false)
			{
				BeginPause();
			}
		}

		if(inputDevice.DPadDown == false)
		{
//			Debug.Log ("dpad up");
			canPressPause = true;
		}

		// a way to pause without a controller
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(canPressPause == true && pauseEnabled == true)
			{
				EndPause();
			} else if (canPressPause == true && pauseEnabled == false)
			{
				BeginPause();
			}
		}
	}

	void BeginPause ()
	{
		// BEGIN PAUSE
		UIRoot.gameObject.SetActive(true);
		pauseEnabled = true;
		canPressPause = false;
		UIRoot.gameObject.GetComponent<TweenAlpha>().PlayForward();
		Time.timeScale = 0;
	}
	
	void EndPause ()
	{
		// END PAUSE
		
		// reset incontrol profile if needed
		GameObject.Find("GameManager").SendMessage("ChangeInputProfile");
		
		// reset incontrol on players
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			player.SendMessage("ResetPlayerInput");
		}
		
		pauseEnabled = false;
		canPressPause = false;
		UIRoot.GetComponent<TweenAlpha>().PlayReverse();
		UIRoot.SetActive(false);
		Time.timeScale = 1;
	}

}
