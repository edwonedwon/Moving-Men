using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {

	public bool debugMode;
	public bool debugView;


	void Start () {
		if (debugMode)
		{
			gameObject.GetComponent<RespawnManager>().enabled = true;
		} else{
			gameObject.GetComponent<RespawnManager>().enabled = false;
		} 

		if (Application.loadedLevelName == "Main Menu")
			gameObject.GetComponent<RespawnManager>().enabled = false;

		// debug view is off disable debug camera
		if (!debugView)
			GameObject.Find ("DebugCamera").gameObject.SetActive(false);
	}
	
	void Update () {
	
	}
}
