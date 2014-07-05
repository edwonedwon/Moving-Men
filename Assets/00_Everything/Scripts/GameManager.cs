using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;
	public bool debugView;

	InputDevice inputDevice;

	void Start () 
	{
		Screen.showCursor = false;
		// debug view is off disable debug camera
		if (!debugView)
			GameObject.Find ("DebugCamera").gameObject.SetActive(false);
	}
	
	void Update () 
	{
		if (Input.GetKey(KeyCode.C))
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players)
			{
				player.SendMessage("ResetPlayerInput");
			}
		}
	}


	
}
