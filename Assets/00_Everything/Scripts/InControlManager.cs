using System;
using UnityEngine;
using InControl;

public class InControlManager : MonoBehaviour
{
	void Start()
	{
		InputManager.Setup();
//		Debug.Log( "InControl (version " + InputManager.Version + ")" );

		// Add a custom device profile.
		InputManager.AttachDevice( new UnityInputDevice( new EdwonInControlProfile() ) );
	}


	void Update()
	{
		InputManager.Update();
	}
}
