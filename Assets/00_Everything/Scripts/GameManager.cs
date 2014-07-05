using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;
	public bool debugView;

	InputDevice inputDevice;

	void Start () 
	{
		// debug view is off disable debug camera
		if (!debugView)
			GameObject.Find ("DebugCamera").gameObject.SetActive(false);
	}
	
	void Update () 
	{
		
	}
	
}
