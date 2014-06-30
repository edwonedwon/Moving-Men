using UnityEngine;
using System.Collections;

public class CameraFollowObject : MonoBehaviour {

	public GameObject follow1;
	public GameObject follow2;

	void Start () 
	{
	
	}
	
	void Update () 
	{
		gameObject.transform.position =  (follow1.transform.position + follow2.transform.position)/2;
	}
}
