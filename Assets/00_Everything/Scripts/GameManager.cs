using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;

	InputDevice inputDevice;

	void Start () 
	{
		Screen.showCursor = false;

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
