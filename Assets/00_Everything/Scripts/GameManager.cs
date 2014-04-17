using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;
	public bool debugView;

	InputDevice inputDevice;

	void Start () 
	{
		// debug view is off disable debug camera
		if (!debugView)
			GameObject.Find ("DebugCamera").gameObject.SetActive(false);
	}
	
	void Update () 
	{
		// inControl setup
		InputManager.Update();
		inputDevice = InputManager.ActiveDevice;
		// if press up on dpad reload level
		if(inputDevice.DPadUp)
		{
			Application.LoadLevel(0);
		}
		
	}
	
}
