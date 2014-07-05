using UnityEngine;
using System.Collections;

public class PauseMenuManager : MonoBehaviour {

	UIPopupList controllersPopupList;
	UIPopupList levelPopupList;

	private bool canChangeController = false;
	private bool canChangeLevel = false;
	private bool startUpCheckLevel = false;
	private bool startUpCheckController = false;

	private string controllerValue;
	public string levelValue;

	GameManager gm;

	void Start ()
	{

	}

	void OnEnable () 
	{
		controllersPopupList = transform.FindChild("Controls").FindChild("Dropdown - Controllers").GetComponent<UIPopupList>();
		levelPopupList = transform.FindChild("Controls").FindChild("Dropdown - Level Select").GetComponent<UIPopupList>();
		EventDelegate.Add(controllersPopupList.onChange, ChangeControllers);
		EventDelegate.Add(levelPopupList.onChange, ChangeLevel);

		gm = GameObject.Find("GameManager").GetComponent<GameManager>();

	}
	
	void Update () 
	{
		// when the controller popup list is closed, change the controller mode
		if (canChangeController && controllersPopupList.isOpen == false)
		{
			canChangeController = false;
			// do the controller mode change
			if (controllerValue == "1 Controller")
			{
//				Debug.Log ("Controller Mode: " + "1 Controller");
				gm.singlePlayer = true;
			}
			if (controllerValue == "2 Controllers")
			{
//				Debug.Log ("Controller Mode: " + "2 Controllers");
				gm.singlePlayer = false;
			}
		}

		// when the level popup list is closed, change the level
		if (canChangeLevel && levelPopupList.isOpen == false)
		{
			canChangeLevel = false;
			// do the level change
//			Debug.Log ("Load Level: " + levelValue);

			if (levelValue != Application.loadedLevelName) 		// if selected a new level
				Application.LoadLevel(levelValue);
		}
	}

	public void ResetLevel ()
	{
		Application.LoadLevel(levelValue);
	}

	void ChangeControllers ()
	{
		if (startUpCheckController == true)
		{
			canChangeController = true;
			controllerValue = UIPopupList.current.value;
	//		Debug.Log("Selection: " + controllerValue);
		}
		startUpCheckController = true;
	}

	void ChangeLevel ()
	{
		if (startUpCheckLevel == true)
		{
			Debug.Log("changed level");
			canChangeLevel = true;
			levelValue = UIPopupList.current.value;
	//		Debug.Log ("Level Selection: " + levelValue);
		}
		startUpCheckLevel = true;
	}
	
	void OnSelectionChange (string selectedItem) 
	{
		print ("selectedItem = " + selectedItem);
	}   
}
