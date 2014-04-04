using System;
using UnityEngine;
using System.Collections;
using InControl;

public class EdwonInputManager : MonoBehaviour {

	void Start () {
		InputManager.Setup();
		InputManager.AttachDevice( new UnityInputDevice (new EdwonInControlProfile()));
		Debug.Log( "InControl (version " + InputManager.Version + ")" );

	}
	
	void Update () {
		InputManager.Update();
		var inputDevice = InputManager.ActiveDevice;

		if (inputDevice.Action1)
		{
			Debug.Log ("pressed something");
		}
	}
}
