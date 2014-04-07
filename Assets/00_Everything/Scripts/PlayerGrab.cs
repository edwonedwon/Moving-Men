using UnityEngine;
using System.Collections;
using InControl;

public class PlayerGrab : MonoBehaviour {

	public string grabState; 
	public bool canGrab;
	public Rigidbody grabThing;
	public Material headMaterial;
	ConfigurableJoint joint;

	InputDevice inputDevice;
	GameManager gameManager;
	PlayerManager playerManager;

	void Start () 
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		playerManager = transform.parent.GetComponent<PlayerManager>();
		joint = transform.parent.GetComponent<ConfigurableJoint>();

		grabState = "grab";
	}
	
	void Update ()
	{
		InputManager.Update();
		inputDevice = GetInputDevice();

		UpdateControls ();
	}

	void UpdateControls ()
	{
		if (gameManager.singlePlayer)// if one controller
		{
			if (inputDevice.Action3 && playerManager.playerIndex == 1)
				Grab ();
			if (inputDevice.Action2 && playerManager.playerIndex == 2)
				Grab ();
		}
		else// if multiple controllers
		{
			if (inputDevice.Action1)
				Grab ();
		}

//		HeadFeedback();
	}

//	void HeadFeedback()
//	{
//		if (canGrab == true)
//		{
//			headMaterial.color = Color.red;
//		}
//		else
//		{
//			headMaterial.color = Color.white;
//		}
//	}

	void Grab ()
	{
		if (joint.connectedBody == null && canGrab == true)
		{
			GrabStart ();
		}
		else
		{
			GrabEnd ();
		}
	
	}

	void GrabStart ()
	{
		canGrab = false;
		grabState = "grab";
		joint.connectedBody = grabThing; 
		joint.xMotion = ConfigurableJointMotion.Locked;
		joint.yMotion = ConfigurableJointMotion.Locked;
		joint.zMotion = ConfigurableJointMotion.Locked;
	}

	void GrabEnd ()
	{
		grabState = "noGrab";
		joint.connectedBody = null;
		joint.xMotion = ConfigurableJointMotion.Free;
		joint.yMotion = ConfigurableJointMotion.Free;
		joint.zMotion = ConfigurableJointMotion.Free;
	}

	void OnTriggerStay(Collider collider)
	{
		if (collider.tag == "GrabZone")
		{
			canGrab = true;
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "GrabZone")
		{
			canGrab = false;
		}
	}

	InputDevice GetInputDevice()
	{
		if (gameManager.singlePlayer)
		{
			return InputManager.ActiveDevice;
		}
		else
		{
			return (InputManager.Devices.Count > playerManager.playerIndex-1) ?
				InputManager.Devices[playerManager.playerIndex-1]: null;
		}
	}
}
