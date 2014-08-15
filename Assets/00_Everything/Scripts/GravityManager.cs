using UnityEngine;
using System.Collections;
using InControl;

public class GravityManager : MonoBehaviour {

	InputDevice inputDevice;

	private Vector3 direction, moveDirection, screenMovementForward, screenMovementRight, movingObjSpeed;
	private GameManager gameManager;
	private PlayerManager playerManager;
	
	float h;
	float v;
	
	void Awake ()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

	}
	
	void Start () 
	{
		inputDevice = GetInputDevice();
		playerManager = GetComponent<PlayerManager>();

		// set gravity to zero
		Physics.gravity = new Vector3(0, 0, 0);

		// disable player move scripts and add player move zero gravity scripts
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
		{
			player.GetComponent<PlayerMove>().enabled = false;
			player.AddComponent<PlayerMoveZeroGravity>();
		}

	}
	
	void Update () 
	{
		if (gameManager.singlePlayer && playerManager.playerIndex == 1)
		{
			h = inputDevice.LeftStickX;
			v = inputDevice.LeftStickY;
			
		} 
		else if (gameManager.singlePlayer && playerManager.playerIndex == 2)
		{
			h = inputDevice.RightStickX;
			v = inputDevice.RightStickY;
		} 
		else if (!gameManager.singlePlayer)
		{
			h = inputDevice.LeftStickX;
			v = inputDevice.LeftStickY;
			
		}

		direction = Vector3.right * h;
		moveDirection = transform.position + direction;
	}

	void FixedUpdate() 
	{

	}

	InputDevice GetInputDevice()
	{
		if (gameManager.singlePlayer)
		{
			return InputManager.ActiveDevice;
		}
		else 
		{
			// THIS OLD CODE AUTOMATICALLY SET THE CONTROLLER BASED ON PLAYER INDEX, BUT MESSES UP SOMETIMES
			//			return (InputManager.Devices.Count > playerManager.playerIndex-1) ?
			//				InputManager.Devices[playerManager.playerIndex-1]: null;
			
			// THIS CODE ALLOWS YOU TO CHOSE THE CONTROLLER PER PLAYER
			return InputManager.Devices[playerManager.controllerIndex];
		}
	}
}
