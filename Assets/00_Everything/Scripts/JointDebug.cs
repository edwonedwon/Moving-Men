using UnityEngine;
using System.Collections;

public class JointDebug : MonoBehaviour {

	GameObject connectedAnchorSphere;

	ConfigurableJoint joint;
	GameManager gameManager;
	DebugManager debugManager;

	void Start () 
	{

		joint = GetComponent<ConfigurableJoint>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		debugManager = GameObject.Find("DebugManager").GetComponent<DebugManager>();
		connectedAnchorSphere = (GameObject)Instantiate(Resources.Load("DebugSphere"));
	}
	
	void Update () 
	{
		if (debugManager.debugView)
		{
			DebugViewUpdate();
		}
	}

	void DebugViewUpdate ()
	{
		// set the connectedAnchorSphere's position
		if (joint.connectedBody != null)
		{
			connectedAnchorSphere.SetActive(true);
			connectedAnchorSphere.transform.position = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
		}
		else
			connectedAnchorSphere.SetActive(false);
	}
}
