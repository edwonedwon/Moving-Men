using UnityEngine;
using System.Collections;

// this script is put on objects that will be carried by the moving men
// if the object bumps into something,
// the "damage" message is sent to the Customer Manager

public class DamageDetector : MonoBehaviour {

	public string hudPathDamaged;
	public string hudPathBumped;
	private GameObject cm; // customer manager

	void Start () 
	{
		cm = GameObject.Find ("CustomerManager");
	}
	
	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Ground")
		{
			Debug.Log ("Plank Touched Ground");
			cm.SendMessage("ShowHudElement", hudPathDamaged);
		}
		if (collision.collider.tag == "Stage")
		{
			Debug.Log ("Plank Touched Stage");
			cm.SendMessage("ShowHudElement", hudPathBumped);
		}
	}
}
