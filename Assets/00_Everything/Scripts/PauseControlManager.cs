using UnityEngine;
using System.Collections;
using InControl;

// THIS SCRIPT ONLY MANAGES THE TURNING ON AND OFF OF THE PAUSE MENU
// FOR THE PAUSE MENU DETAILED CONTROLS SEE PAUSE MENU MANAGER

public class PauseControlManager : MonoBehaviour {

	private bool pauseEnabled;
	private bool canPressPause;

	public bool mainMenuMode = false;
	
	InputDevice inputDevice;

	GameObject UIRoot;

	void Start () 
	{
		pauseEnabled = false;
		canPressPause = true;
		Time.timeScale = 1;
		UIRoot = transform.GetChild(0).gameObject;
		InputManager.Setup();

		if(mainMenuMode)
			MainMenuSetup();
		else
			transform.GetComponent<UIPanel>().alpha = 1;
	}

	void Update () 
	{
//		Debug.Log("pauseEnabled = " + pauseEnabled);
//		Debug.Log("canPressPause = " + canPressPause);

		InputManager.Update();
		inputDevice = InputManager.ActiveDevice;

		if(inputDevice.DPadDown && !mainMenuMode)
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

		if(inputDevice.DPadDown == false && !mainMenuMode)
		{
//			Debug.Log ("dpad up");
			canPressPause = true;
		}

		// a way to pause without a controller
		if(Input.GetKeyDown(KeyCode.P) && !mainMenuMode)
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
		EnablePlayerControls(false);
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

		EnablePlayerControls(true);

		pauseEnabled = false;
		canPressPause = false;
		UIRoot.GetComponent<TweenAlpha>().PlayReverse();
		UIRoot.SetActive(false);
		Time.timeScale = 1;
	}

	void EnablePlayerControls (bool enable)
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			player.transform.FindChild("GrabBox").GetComponent<PlayerGrab>().enabled = enable;
		}
	}

	void MainMenuSetup ()
	{
		UIRoot.SetActive(true);
		UIRoot.GetComponent<UIPanel>().alpha = 1;
		transform.GetComponent<UIPanel>().alpha = 1;
//		GameObject.Find("BG - Vignette").SetActive(false);
//		GameObject.Find("Instructions").SetActive(false);
	}

}
