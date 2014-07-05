using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {

	public bool debugMode;

	void Start () {
		if (debugMode)
		{
			gameObject.GetComponent<RespawnManager>().enabled = true;
		} else{
			gameObject.GetComponent<RespawnManager>().enabled = false;
		}
	}
	
	void Update () {
	
	}
}
