using UnityEngine;
using System.Collections;

// this script is put on objects that will be carried by the moving men
// if the object bumps into something,
// the "damage" message is sent to the Customer Manager

public class DamageDetector : MonoBehaviour {

	public string hudPathDamaged;
	public float damageBill;
//	public string hudPathBumped;
//	public float bumpBill;

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
			cm.SendMessage("ShowHudElement", hudPathDamaged);
			cm.SendMessage("LoseMoney", damageBill);
		}

//		if (collision.collider.tag == "Stage")
//		{
//			Debug.Log ("Plank Touched Stage");
//			cm.SendMessage("ShowHudElement", hudPathBumped);
//			cm.SendMessage("LoseMoney", bumpBill);
//		}
	}
}
