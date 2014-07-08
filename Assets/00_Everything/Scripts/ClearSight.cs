using UnityEngine;
using System.Collections;

public class ClearSight : MonoBehaviour
{
	public GameObject mustSee;
	public float castRadius;

	void Update()
	{
		RaycastHit[] hits;
		// you can also use CapsuleCastAll()
		// TODO: setup your layermask it improve performance and filter your hits.
		Vector3 heading = mustSee.transform.position - transform.position;
		float distance = heading.magnitude;
		hits = Physics.SphereCastAll(transform.position, castRadius, heading, distance);
		DebugExtension.DebugCapsule(transform.position, mustSee.transform.position, Color.red, castRadius);
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
}