using UnityEngine;
using System.Collections;

// this script handles the grip meter
// which goes down depending on how heave the object is
// and how long you've been holding it

public class GripTimer : MonoBehaviour {

	public float grip;
	public float gripLossSpeed;
	public string gripState;
	private  float maxGrab = 1; 
	
	private PlayerGrab grab;

	void Start () {

		Transform grabBox = transform.FindChild("GrabBox");
		grab = grabBox.transform.GetComponent<PlayerGrab>();
	}
	
	void Update () {

		// math to make grip go down over time
		if (grab.grabState == "grabbing" && grip > 0)
		{
			grip -= gripLossSpeed * Time.deltaTime;
			if (grip < 0)
				grip = 0;
		}

		// state change logic
		if (grip > .65)
			gripState = "good";
		if (grip > .35 && grip < .65)
			gripState = "medium";
		if (grip > .15 && grip < .35)
			gripState = "bad";
		if (grip > 0 && grip < .15) 
			gripState = "panic";
		if (grip == 0)
			gripState = "faint";

		// state change visual feedback
		if (gripState == "good")
		{

		}
		if (gripState == "medium")
		{
			
		}
		if (gripState == "bad")
		{
			
		}
		if (gripState == "panic")
		{
			
		}
		if (gripState == "faint")
		{

		}
	}
}
