using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BuildCircleMesh))]

public class CircleAnimator : MonoBehaviour {

	public float growSpeed;
	public float circleFinalRadius;
	public float circleFinalWidth;
	public float timeGrowWidth;
	public float fadeSpeed;

	float startTime;
	
	BuildCircleMesh circle;

	void Start () 
	{
		circle = GetComponent<BuildCircleMesh>();
		startTime = Time.time;
	}
	
	void Update () 
	{
		float rad = (Time.time - startTime) / growSpeed;
		circle.innerRadius = Mathf.Lerp(0, circleFinalRadius, rad);

		float width = (Time.time - startTime) / timeGrowWidth;
		circle.circleWidth =  Mathf.Lerp(0, circleFinalWidth, width);

		float a = (Time.time - startTime) / fadeSpeed;
		float mainAlpha = Mathf.Lerp (1, 0, a);
		renderer.material.SetColor("_Color", new Color(255,255,255,mainAlpha));

		if ((Time.time - startTime) > fadeSpeed)
			Destroy (gameObject);
	}
}
