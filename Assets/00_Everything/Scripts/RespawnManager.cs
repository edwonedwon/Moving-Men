using UnityEngine;
using System.Collections;
using InControl;
using System.Linq;
using System.Collections.Generic;

// This script manages the debug mode where you can go back and forth 
// between player spawn points using the dpad
// used primarily for level design

public class RespawnManager : MonoBehaviour {

//	public bool canRespawn;

	private GameObject[] spawnPlayerPoints;

	private int spawnPlayerListPosition;
	private int spawnPlayerPointsTotal;

	InputDevice inputDevice;

	private bool canPressLeft = true;
	private bool canPressRight = true;

	private GameObject players;


	void Start () 
	{
		players = GameObject.Find("Players");

		// set and sort the array by the spawn order number set in the inspector
		spawnPlayerPoints = GameObject.FindGameObjectsWithTag("SpawnPlayer").OrderBy(
			spawnPlayerPoint => spawnPlayerPoint.GetComponent<SpawnPlayerPoint>().spawnOrder).ToArray();
		// set the players current position in that order
		spawnPlayerListPosition = spawnPlayerPoints.Length-1;
		spawnPlayerPointsTotal = spawnPlayerPoints.Length-1;
	}
	
	void Update () 
	{
		InputManager.Update();
		inputDevice = InputManager.ActiveDevice;

		// logic for choosing which point to spawn too when pressing left and right on the DPad

		if(inputDevice.DPadLeft && canPressLeft == true)
		{
			canPressLeft = false;
			if (spawnPlayerListPosition > 0)
			{
				spawnPlayerListPosition -= 1;
		    }
			ResetPlayersPosition();
			players.SetActive(false);
			players.transform.position = spawnPlayerPoints[spawnPlayerListPosition].transform.position;
			players.SetActive(true);

		}
		if(inputDevice.DPadRight && canPressRight == true)
		{
			canPressRight = false;
			if (spawnPlayerListPosition < spawnPlayerPoints.Length-1)
			{
				spawnPlayerListPosition += 1;
			}
			ResetPlayersPosition();
			players.SetActive(false);
			players.transform.position = spawnPlayerPoints[spawnPlayerListPosition].transform.position;
			players.SetActive(true);

		}
		if(!inputDevice.DPadLeft && !inputDevice.DPadRight)
		{
			canPressLeft = true;
			canPressRight = true;
		}
	}


	void ResetPlayersPosition ()
	{
		// first put all the child objects of players parent object into an array

		List<Transform> children = new List<Transform>();
		foreach (Transform child in players.transform)
		{
			children.Add(child);
//			Debug.Log (child.name);
		}

		// then deparent all the child objects of players parent object
		players.transform.DetachChildren();

		// then center the players parent object to the middle of all the children
		Vector3 centroid = new Vector3 (0,0,0);
		foreach (Transform child in children)
			centroid += child.position;
		centroid /= children.Count;
		players.transform.position = centroid;

		// then re-parent the children to the players parent object
		foreach (Transform child in children)
		{
			child.parent = players.transform;
		}
	}


}
