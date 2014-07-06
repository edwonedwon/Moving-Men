using UnityEngine;
using System.Collections;

public class CustomerManager : MonoBehaviour {

	public float movingFee;
	public float damageHudLifetime;
	private Transform hud;
	private UILabel hudFee;

	private GameManager gm;

	void Start () 
	{
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		hud = GameObject.Find("HUD").transform;
		hudFee = hud.FindChild("UI/Top Left Anchor/Fee").GetComponent<UILabel>();
	}
	
	void Update () 
	{
		hudFee.text = "$" + movingFee.ToString();
		if (movingFee <= 0)
		{
			gm.SendMessage("OutOfMoney");
		}
	}

	void LoseMoney (float bill)
	{
		movingFee -= bill;
	}

	void ShowHudElement (string hudPath)
	{
//		Debug.Log ("Message Received" + damaged);
		Transform hudElement = hud.FindChild(hudPath);
		TurnOffOtherHudElements(hudElement);
		hudElement.GetComponent<TweenAlpha>().PlayForward();
		StartCoroutine(HudTimer(hudElement));
	}

	void TurnOffOtherHudElements (Transform hudElement)
	{
		// this function goes through all hud elements 
		// in the same level of heirarchy
		// and turns them off
		foreach (Transform child in hudElement.parent)
		{
			if (child.GetComponent<TweenAlpha>().value != 0)
				child.GetComponent<TweenAlpha>().PlayReverse();
		}
	}

	void HideHudElement ()
	{

	}

	IEnumerator HudTimer (Transform hudElement)
	{
		yield return new WaitForSeconds(damageHudLifetime);
		hudElement.GetComponent<TweenAlpha>().PlayReverse();
	}

}
