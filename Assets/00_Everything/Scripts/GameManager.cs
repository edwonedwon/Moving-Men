using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;

	InputDevice inputDevice;

	void Start () 
	{

	}
	
	void Update () 
	{
		InputManager.Update();
		inputDevice = InputManager.ActiveDevice;

		if(inputDevice.DPadUp)
		{
			Application.LoadLevel(0);
		}
		
	}
	
}
