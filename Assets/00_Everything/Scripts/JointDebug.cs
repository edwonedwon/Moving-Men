using UnityEngine;
using System.Collections;

public class JointDebug : MonoBehaviour {

	GameObject connectedAnchorSphere;

	ConfigurableJoint joint;
	GameManager gameManager;

	void Start () 
	{

		joint = GetComponent<ConfigurableJoint>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		connectedAnchorSphere = (GameObject)Instantiate(Resources.Load("DebugSphere"));
	}
	
	void Update () 
	{
		if (gameManager.debugView)
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
