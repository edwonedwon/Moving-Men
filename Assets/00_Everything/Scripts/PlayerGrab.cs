using UnityEngine;
using System.Collections;
using InControl;

public class PlayerGrab : MonoBehaviour {

	public string grabState; 
	public Rigidbody grabThing;
	public Material feedbackMaterial;
	ConfigurableJoint joint;

	bool canPressAction3;
	bool canPressAction2;

	InputDevice inputDevice;
	GameManager gameManager;
	PlayerManager playerManager;

	void Start () 
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		playerManager = transform.parent.GetComponent<PlayerManager>();
		joint = transform.parent.GetComponent<ConfigurableJoint>();

		grabState = "grabbing";
		canPressAction3 = true;
		canPressAction2 = true;
	}
	
	void Update ()
	{

		InputManager.Update();
		inputDevice = GetInputDevice();

		UpdateControls();
		HeadFeedback();

//		print (grabState);
	
	}

	void UpdateControls ()
	{
		if (gameManager.singlePlayer)// if one controller
		{
			if (canPressAction3 && inputDevice.Action3 && playerManager.playerIndex == 1)
			{
				canPressAction3 = false;
				Grab ();
			}
			if (inputDevice.Action2 && playerManager.playerIndex == 2)
			{
				canPressAction2 = false;
				Grab ();
			}
			if (!canPressAction3 && !inputDevice.Action3  && playerManager.playerIndex == 1)
				canPressAction3 = true;
			if (!canPressAction2 && !inputDevice.Action2  && playerManager.playerIndex == 2)
				canPressAction2 = true;
		}
		else// if multiple controllers
		{
			if (inputDevice.Action1)
				Grab ();
		}
	}

	void HeadFeedback()
	{
		if (grabState == "notGrabbingCanGrab")
		{
			feedbackMaterial.color = Color.red;
		} 
		else 
		{
			feedbackMaterial.color = Color.white;
		}
	}

	void Grab ()
	{
		print ("did grab");
		if (joint.connectedBody == null && grabState == "notGrabbingCanGrab")
		{
			print ("did grab start");
			GrabStart ();
		}
		else
		{
			print ("did grab end");
			GrabEnd ();
		}
	
	}

	void GrabStart ()
	{
		grabState = "grabbing";
		joint.connectedBody = grabThing; 
		joint.xMotion = ConfigurableJointMotion.Locked;
		joint.yMotion = ConfigurableJointMotion.Locked;
		joint.zMotion = ConfigurableJointMotion.Locked;
	}

	void GrabEnd ()
	{
		grabState = "notGrabbing";
		joint.connectedBody = null;
		joint.xMotion = ConfigurableJointMotion.Free;
		joint.yMotion = ConfigurableJointMotion.Free;
		joint.zMotion = ConfigurableJointMotion.Free;
	}

	void OnTriggerEnter(Collider collider)
	{

		if (grabState == "notGrabbing")
		{
			if (collider.tag == "GrabZone")
			{
				print ("entered grab zone");
				grabState = "notGrabbingCanGrab";
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{

		if (collider.tag == "GrabZone")
		{
			print ("exited grab zone");
			grabState = "notGrabbing";
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
