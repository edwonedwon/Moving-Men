//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using InControl;

/// <summary>
/// Attaching this script to a widget makes it react to key events such as tab, up, down, etc.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Key Navigation")]
public class UIKeyNavigation : MonoBehaviour
{

	/// <summary>
	/// List of all the active UINavigation components.
	/// </summary>
	InputDevice inputDevice;

	static public BetterList<UIKeyNavigation> list = new BetterList<UIKeyNavigation>();

	bool canPressLeft = true;
	bool canPressRight = true;
	bool canPressUp = true;
	bool canPressDown = true;

	void Update()
	{
		InputManager.Update();
		inputDevice = InputManager.Devices[0];

		UpdateInControlLogic ();

	}

	void UpdateInControlLogic ()
	{

		GameObject go = null;
		
		if (canPressLeft == true && inputDevice.LeftStickX < -0.9f)
		{
			canPressLeft = false;
//			Debug.Log ("left: " + gameObject.name);
			go = GetLeft();
		}
		
		if (canPressRight == true && inputDevice.LeftStickX > 0.9f)
		{
			canPressRight = false;
//			Debug.Log ("right: " + gameObject.name);
			go = GetRight();
		}
		
		if (canPressUp == true && inputDevice.LeftStickY > 0.95f)
		{
			canPressUp = false;
//			Debug.Log ("up: " + gameObject.name);
			go = GetUp();
		}
		
		if (canPressDown == true && inputDevice.LeftStickY < -0.95f)
		{
			canPressDown = false;
//			Debug.Log ("down: " + gameObject.name);
			go = GetDown();
		}
		
		if (inputDevice.LeftStickY == 0)
		{
			canPressUp = true;
			canPressDown = true;
		}
		
		if (inputDevice.LeftStickX == 0)
		{
			canPressLeft = true;
			canPressRight = true;
		}
		
		if (go != null) UICamera.selectedObject = go;

	}

	public enum Constraint
	{
		None,
		Vertical,
		Horizontal,
		Explicit,
	}

	/// <summary>
	/// If a selection target is not set, the target can be determined automatically, restricted by this constraint.
	/// 'None' means free movement on both horizontal and vertical axis. 'Explicit' means the automatic logic will
	/// not execute, and only the explicitly set values will be used.
	/// </summary>

	public Constraint constraint = Constraint.None;

	/// <summary>
	/// Which object will be selected when the Up button is pressed.
	/// </summary>

	public GameObject onUp;

	/// <summary>
	/// Which object will be selected when the Down button is pressed.
	/// </summary>

	public GameObject onDown;

	/// <summary>
	/// Which object will be selected when the Left button is pressed.
	/// </summary>

	public GameObject onLeft;

	/// <summary>
	/// Which object will be selected when the Right button is pressed.
	/// </summary>

	public GameObject onRight;

	/// <summary>
	/// Which object will get selected on click.
	/// </summary>

	public GameObject onClick;

	/// <summary>
	/// Whether the object this script is attached to will get selected as soon as this script is enabled.
	/// </summary>

	public bool startsSelected = false;

	protected virtual void OnEnable ()
	{

		InputManager.Setup();

		list.Add(this);

		if (startsSelected)
		{
#if UNITY_EDITOR
			if (!Application.isPlaying) return;
#endif
			if (UICamera.selectedObject == null || !NGUITools.GetActive(UICamera.selectedObject))
			{
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.selectedObject = gameObject;
			}
		}
	}

	protected virtual void OnDisable () { list.Remove(this); }

	protected GameObject GetLeft ()
	{
		if (NGUITools.GetActive(onLeft)) return onLeft;
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit) return null;
		return Get(Vector3.left, true);
	}

	GameObject GetRight ()
	{
		if (NGUITools.GetActive(onRight)) return onRight;
		if (constraint == Constraint.Vertical || constraint == Constraint.Explicit) return null;
		return Get(Vector3.right, true);
	}

	protected GameObject GetUp ()
	{
		if (NGUITools.GetActive(onUp)) return onUp;
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit) return null;
		return Get(Vector3.up, false);
	}

	protected GameObject GetDown ()
	{
		if (NGUITools.GetActive(onDown)) return onDown;
		if (constraint == Constraint.Horizontal || constraint == Constraint.Explicit) return null;
		return Get(Vector3.down, false);
	}

	protected GameObject Get (Vector3 myDir, bool horizontal)
	{
		Transform t = transform;
		myDir = t.TransformDirection(myDir);
		Vector3 myCenter = GetCenter(gameObject);
		float min = float.MaxValue;
		GameObject go = null;

		for (int i = 0; i < list.size; ++i)
		{
			UIKeyNavigation nav = list[i];
			if (nav == this) continue;

			// Ignore disabled buttons
			UIButton btn = nav.GetComponent<UIButton>();
			if (btn != null && !btn.isEnabled) continue;

			// Reject objects that are not within a 45 degree angle of the desired direction
			Vector3 dir = GetCenter(nav.gameObject) - myCenter;
			float dot = Vector3.Dot(myDir, dir.normalized);
			if (dot < 0.707f) continue;

			// Exaggerate the movement in the undesired direction
			dir = t.InverseTransformDirection(dir);
			if (horizontal) dir.y *= 2f;
			else dir.x *= 2f;

			// Compare the distance
			float mag = dir.sqrMagnitude;
			if (mag > min) continue;
			go = nav.gameObject;
			min = mag;
		}
		return go;
	}

	static protected Vector3 GetCenter (GameObject go)
	{
		UIWidget w = go.GetComponent<UIWidget>();

		if (w != null)
		{
			Vector3[] corners = w.worldCorners;
			return (corners[0] + corners[2]) * 0.5f;
		}
		return go.transform.position;
	}

//	protected virtual void OnKey (KeyCode key)
//	{
//		if (!NGUITools.GetActive(this)) return;
//
//		GameObject go = null;
//
//		switch (key)
//		{
//		case KeyCode.LeftArrow:
//			go = GetLeft();
//			break;
//		case KeyCode.RightArrow:
//			go = GetRight();
//			break;
//		case KeyCode.UpArrow:
//			go = GetUp();
//			break;
//		case KeyCode.DownArrow:
//			go = GetDown();
//			break;
//		case KeyCode.Tab:
//			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
//			{
//				go = GetLeft();
//				if (go == null) go = GetUp();
//				if (go == null) go = GetDown();
//				if (go == null) go = GetRight();
//			}
//			else
//			{
//				go = GetRight();
//				if (go == null) go = GetDown();
//				if (go == null) go = GetUp();
//				if (go == null) go = GetLeft();
//			}
//			break;
//		}
//
//		if (go != null) UICamera.selectedObject = go;
//	}

	protected virtual void OnClick ()
	{
		if (NGUITools.GetActive(this) && NGUITools.GetActive(onClick))
			UICamera.selectedObject = onClick;
	}
}