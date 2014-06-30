using UnityEngine;
using InControl;

public class CameraFollow : MonoBehaviour 
{
	InputDevice inputDevice;

	public Transform target;									//object camera will focus on and follow
	public Vector3 targetOffset =  new Vector3(0f, 3.5f, 7);	//how far back should camera be from the lookTarget
	public bool lockRotation;									//should the camera be fixed at the offset (for example: following behind the player)
	public float followSpeed = 6;								//how fast the camera moves to its intended position
	public float inputRotationSpeed = 100;						//how fast the camera rotates around lookTarget when you press the camera adjust buttons
	public bool mouseFreelook;									//should the camera be rotated with the mouse? (only if camera is not fixed)
	public float rotateDamping = 100;							//how fast camera rotates to look at target
	public GameObject waterFilter;								//object to render in front of camera when it is underwater
	public string[] avoidClippingTags;							//tags for big objects in your game, which you want to camera to try and avoid clipping with
	
	private Transform followTarget;
	private bool camColliding;

	Bounds playersBounds = new Bounds();
	GameObject[] players = new GameObject[4];
	GameObject playersParent;


	private GameManager gameManager;


	//setup objects
	void Awake()
	{
		playersParent = GameObject.Find("Players");
		// find all players and make a bounds around them
		for (int i = 0; i < playersParent.transform.childCount ; i++)
		{
			players[i] = playersParent.transform.GetChild(0).gameObject;
			playersBounds.Encapsulate(players[i].transform.position);
//			Debug.Log (players[i]);
		}

//		InputManager.AttachDevice( new UnityInputDevice (new EdwonInControlProfile()));

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		followTarget = new GameObject().transform;	//create empty gameObject as camera target, this will follow and rotate around the player
		followTarget.name = "Camera Target";
		if(waterFilter)
			waterFilter.renderer.enabled = false;
		if(!target)
			Debug.LogError("'CameraFollow script' has no target assigned to it", transform);
		
		//don't smooth rotate if were using mouselook
		if(mouseFreelook)
			rotateDamping = 0f;
	}
	
	//run our camera functions each frame
	void Update()
	{
		// WORKING ON CAMERA TRACKING OF BOTH PLAYERS
//		for (int i = 0; i < playersParent.transform.childCount ; i++)
//		{
//			playersBounds.Encapsulate(players[i].transform.position);
//		}
//		Debug.Log(playersBounds.center);


		InputManager.Update();

		inputDevice = InputManager.ActiveDevice;


		if (!target)
			return;
		
		SmoothFollow ();
		if(rotateDamping > 0)
			SmoothLookAt();
		else
			transform.LookAt(target.position);
	}

	//toggle waterfilter, is camera clipping walls?
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Water" && waterFilter)
			waterFilter.renderer.enabled = true;
	}
	
	//toggle waterfilter, is camera clipping walls?
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Water" && waterFilter)
			waterFilter.renderer.enabled = false;
	}
	
	//rotate smoothly toward the target
	void SmoothLookAt()
	{
		Quaternion rotation = Quaternion.LookRotation (target.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, rotateDamping * Time.deltaTime);
	}
		
	//move camera smoothly toward its target
	void SmoothFollow()
	{
		//move the followTarget (empty gameobject created in awake) to correct position each frame
		followTarget.position = target.position;
		followTarget.Translate(targetOffset, Space.Self);
		if (lockRotation)
			followTarget.rotation = target.rotation;
		
//		if(mouseFreelook)
//		{
//			//mouse look
//			float axisX = Input.GetAxis ("Mouse X") * inputRotationSpeed * Time.deltaTime;
//			followTarget.RotateAround (target.position,Vector3.up, axisX);
//			float axisY = Input.GetAxis ("Mouse Y") * inputRotationSpeed * Time.deltaTime;
//			followTarget.RotateAround (target.position, transform.right, -axisY);
//		}
//		else if (gameManager.singlePlayer == false)
//		{
			//keyboard camera rotation look
//			float axis = Input.GetAxis ("CamHorizontal") * inputRotationSpeed * Time.deltaTime;
			float bumperAxis = -inputDevice.RightBumper + inputDevice.LeftBumper;
			float axis = bumperAxis * inputRotationSpeed * Time.deltaTime;
			followTarget.RotateAround (target.position, Vector3.up, axis);
//		}
		
		//where should the camera be next frame?
		Vector3 nextFramePosition = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
		Vector3 direction = nextFramePosition - target.position;
		//raycast to this position
		RaycastHit hit;
		if(Physics.Raycast (target.position, direction, out hit, direction.magnitude + 0.3f))
		{
			transform.position = nextFramePosition;
			foreach(string tag in avoidClippingTags)
				if(hit.transform.tag == tag)
					transform.position = hit.point - direction.normalized * 0.3f;
		}
		else
		{
			//otherwise, move cam to intended position
			transform.position = nextFramePosition;
		}
	}
}