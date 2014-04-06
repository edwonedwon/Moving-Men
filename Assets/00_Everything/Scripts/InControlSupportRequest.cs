/*

using UnityEngine;
using System.Collections;

public class inControlSupportRequest : MonoBehaviour {

	void Awake()
	{	
		// inControl setup
		InputManager.Setup();
		InputManager.AttachDevice( new UnityInputDevice (new EdwonInControlProfile()));

		// single or multiple controllers logic
		if (gameManager.singlePlayer == true)
		{
			inputDevice = InputManager.Devices[0];
			//			Debug.Log (InputManager.Devices[0].Meta);
		} else if (gameManager.singlePlayer == false)
		{
			inputDevice = InputManager.Devices[playerManager.playerIndex-1];
		}
	}
	
	float h;
	float v;
	
	//get state of player, values and input
	void Update()
	{	
		InputManager.Update();

		// if I am the "player 1" prefab, I am controlled by left stick of controller 1
		if (gameManager.singlePlayer == true && playerManager.playerIndex == 1)
		{
			Debug.Log ("single controller left");
			// single controller left stick
			h = inputDevice.LeftStickX;
			v = inputDevice.LeftStickY;
			Debug.Log ("h: " + h + "v: " + v);

		} 
		// if I am the "player 2" prefab, I am controlled by right stick of controller 1
		else if (gameManager.singlePlayer == true && playerManager.playerIndex == 2)
		{
			Debug.Log ("single controller right");
			// single controller right stick
			h = inputDevice.RightStickX;
			v = inputDevice.RightStickY;
			Debug.Log ("h: " + h + "v: " + v);
		} 
		// 
		else if (gameManager.singlePlayer == false)
		{
			Debug.Log ("multi controllers");
			// multiple controllers
			h = inputDevice.LeftStickX;
			v = inputDevice.LeftStickY;
			Debug.Log ("h: " + h + "v: " + v);

		}

	}
}

 */
