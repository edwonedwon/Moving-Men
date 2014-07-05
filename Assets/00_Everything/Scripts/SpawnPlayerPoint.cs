using UnityEngine;
using System.Collections;

public class SpawnPlayerPoint : MonoBehaviour {

	public int spawnOrder;

	private float gizmoSize = 1;

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 0, 0, 0.5F);
		Gizmos.DrawCube(transform.position, new Vector3(gizmoSize, gizmoSize, gizmoSize));
	}
}
