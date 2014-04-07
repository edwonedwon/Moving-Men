using System;
using UnityEngine;
using InControl;

public class InControlManager : MonoBehaviour
{
	void Start()
	{
		InputManager.Setup();
//		Debug.Log( "InControl (version " + InputManager.Version + ")" );
	}


	void Update()
	{
		InputManager.Update();
	}
}
