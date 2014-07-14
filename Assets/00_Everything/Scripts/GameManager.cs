using UnityEngine;
using System.Collections;
using InControl;

public class GameManager : MonoBehaviour {

	public bool singlePlayer;

	private Transform hud;

	InputDevice inputDevice;

	void Start () 
	{
		Screen.showCursor = false;
		if(GameObject.Find ("HUD") != null)
			hud = GameObject.Find("HUD").transform;

	}
	
	void Update () 
	{

		if (Input.GetKey(KeyCode.C))
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players)
			{
				player.SendMessage("ResetPlayerInput");
			}
		}
	}

	void OutOfMoney ()
	{
		Time.timeScale = 0;
		hud.FindChild("UI/Center Anchor/Game Over").GetComponent<UITweener>().PlayForward();
		// turn off any hud elements except for the "fee" hud element

//		TweenAlpha[] tweens = hud.FindChild("UI").GetComponentsInChildren<TweenAlpha>();
//		for (int i = 0; i<tweens.Length ; i++)
//		{
//			tweens[i].PlayReverse();
//		}

		foreach (Transform child in hud.FindChild("UI"))
		{
			foreach (Transform childofchild in child)
			{
				if (childofchild.GetComponent<TweenAlpha>() != null 
				    && childofchild.name != "Fee" 
				    && childofchild.name != "Game Over")
					childofchild.GetComponent<TweenAlpha>().PlayReverse();
			}
		}
	}
	
}
