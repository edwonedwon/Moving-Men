using UnityEngine;
using System.Collections;

// this script should be placed on the main camera
// it turns objects transparent when they are between camera and player
// by attaching the AutoTransparent script to the objects
// while they are in the way

public class ClearSight : MonoBehaviour
{
	public GameObject mustSee;
	public float castRadius;

	public GameObject lastGround;
	public string lastGroundTag;
	public GameObject currentGround;
	public string currentGroundTag;


	void Start()
	{
		lastGround = GameObject.Find ("ground");
		lastGroundTag = "Ground";
	}

	void Update()
	{
		PlayerFeetChangeTag();

		RaycastHit[] hits;
		// you can also use CapsuleCastAll()
		// TODO: setup your layermask it improve performance and filter your hits.
		Vector3 heading = mustSee.transform.position - transform.position;
		float distance = heading.magnitude;
		hits = Physics.SphereCastAll(transform.position, castRadius, heading, distance);
		DebugExtension.DebugWireSphere(transform.position, Color.red, castRadius);
		foreach(RaycastHit hit in hits)
		{
			Renderer R = hit.collider.renderer;
			if (R == null)
				continue; // no renderer attached? go to next hit
			// TODO: maybe implement here a check for GOs that should not be affected like the player

			if (hit.collider.tag != "Ground" && hit.collider.tag != "Floor")
			{
				AutoTransparent AT = R.GetComponent<AutoTransparent>();
				if (AT == null) // if no script is attached, attach one
				{
					AT = R.gameObject.AddComponent<AutoTransparent>();
				}
				AT.BeTransparent(); // get called every frame to reset the falloff
			}
		}
	}

	public bool canChangeLastTag = true;

	void PlayerFeetChangeTag ()
	{
		// this function changes anything the players are standing on to a "Ground" tag
		// that way whatever is below the players is always opaque
//		if (currentGround != lastGround)
//		{
////			Debug.Log ("currentGround != lastGround");
//			lastGround.tag = lastGroundTag;
//		}

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players)
		{
			RaycastHit hit;
			if (Physics.Raycast(player.transform.position, Vector3.down, out hit, 100f))
			{

				currentGround = hit.collider.gameObject;
				currentGroundTag = hit.collider.tag;
				hit.collider.tag = "Ground";
				if (currentGround != lastGround)
				{
					lastGround.tag = lastGroundTag;
					lastGround = currentGround;
					lastGroundTag = currentGroundTag;
				}
			}
		}

		// if the object your standing on changes, change the lastGround tag back to what it was

	}
}