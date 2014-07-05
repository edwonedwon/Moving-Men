using System;
using UnityEngine;
using InControl;

public class InControlManager : MonoBehaviour
{
	public bool useKeyboard;

	void Start()
	{
		InputManager.Setup();
//		Debug.Log( "InControl (version " + InputManager.Version + ")" );

		ChangeInputProfile ();
	}

	public void ChangeInputProfile ()
	{
		if (useKeyboard)
		{
			print ("using keyboard");
			InputManager.AttachDevice( new UnityInputDevice( new EdwonInControlProfile() ) );
		}

	}


	void Update()
	{
		InputManager.Update();
	}
}
