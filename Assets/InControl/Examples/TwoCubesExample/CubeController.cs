using System;
using UnityEngine;
using InControl;


namespace TwoCubesExample
{
	public class CubeController : MonoBehaviour
	{
		public int playerNum;
		Vector3 targetPosition;
		GameManager gameManager;


		void Start()
		{
			gameManager = GameManager.instance;
		}


		void Update()
		{
			InputManager.Update();

			var inputDevice = GetInputDevice();
			if (inputDevice == null)
			{
				// If no controller exists for this cube, just make it semi-transparent.
				renderer.material.color = new Color( 1.0f, 1.0f, 1.0f, 0.2f );
			}
			else
			{
				UpdateCubeWithInputDevice( inputDevice );
			}
		}


		InputDevice GetInputDevice()
		{
			if (gameManager.isSinglePlayer)
			{
				return InputManager.ActiveDevice;
			}
			else
			{
				return (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
			}
		}


		void UpdateCubeWithInputDevice( InputDevice inputDevice )
		{
			// Set target object material color based on which action is pressed.
			if (inputDevice.Action1)
			{
				renderer.material.color = Color.green;
			}
			else
			if (inputDevice.Action2)
			{
				renderer.material.color = Color.red;
			}
			else
			if (inputDevice.Action3)
			{
				renderer.material.color = Color.blue;
			}
			else
			if (inputDevice.Action4)
			{
				renderer.material.color = Color.yellow;
			}
			else
			{
				renderer.material.color = Color.white;
			}

			if (gameManager.isSinglePlayer && playerNum == 1)
			{
				transform.Rotate( Vector3.down,  500.0f * Time.deltaTime * inputDevice.RightStickX, Space.World );
				transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.RightStickY, Space.World );
			}
			else
			{
				transform.Rotate( Vector3.down,  500.0f * Time.deltaTime * inputDevice.LeftStickX, Space.World );
				transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.LeftStickY, Space.World );
			}
		}
	}
}

