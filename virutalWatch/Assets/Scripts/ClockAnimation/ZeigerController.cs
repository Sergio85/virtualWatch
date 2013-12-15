using UnityEngine;
using System;
using System.Collections;
using Leap;

public class ZeigerController : MonoBehaviour {
	
	public GameObject minuteHand;
	public GameObject hourHand;
	public GameObject secondHand;
	
	public Material interactionMaterial;
	private Material defaultMaterial;
	
	//Lupe
	private GameObject magnifier;
	
	
	
	public int secondsUntilReset = 10;
	
	private Vector2 previousPos;
	private Vector2 center;
	
	public const float MAX_ANGLE_STEP = 5.0f;
	public const float IGNORE_INPUT_ANGLE = 15.0f;
	
	
	// Use this for initialization
	void Start () {
		
		magnifier = hourHand.transform.Find("lupe").gameObject;
		defaultMaterial = magnifier.renderer.material;
		
		
		center = new Vector2(transform.position.x, transform.position.y);
		LeapInput.PointingFingerFound += HandleLeapInputPointingFingerFound;
		LeapInput.PointingFingerLost += HandleLeapInputPointingFingerLost;
		LeapInput.PointingFingerUpdated += HandleLeapInputPointingFingerUpdated;
		
		secondHand.transform.Rotate(Vector3.forward, CalculateSecondHandRotation());	
		ResetClock();
 
	}

	void HandleLeapInputPointingFingerUpdated (Leap.Pointable p)
	{
		
		Vector2 currentPos = CenterToFingerTip(p.StabilizedTipPosition.ToUnityTranslated());
		float angle = Vector2.Angle(previousPos, currentPos);
		
		//Keine Rotation bei ruckartigen Bewegungen
		if(angle > IGNORE_INPUT_ANGLE)
			return;
		
		//Zu schnelles Drehen begrenzen
		angle = Math.Min(angle, MAX_ANGLE_STEP);
		setHourHandColor(angle);
		
		
		Vector3 rotationAxis = Vector3.Cross(previousPos, currentPos);
		
		hourHand.transform.Rotate(rotationAxis, angle );
		minuteHand.transform.Rotate(rotationAxis, angle * 12);
		
		previousPos = currentPos;
	}
	
	private void ResetClock(){
			
		//set rotation to current time
		iTween.RotateTo(hourHand.gameObject, new Vector3(0,0, CalculateHourHandRotation()), 1f);
		iTween.RotateTo(minuteHand.gameObject, new Vector3(0,0, CalculateMinuteHandRotation()), 1f);
		
		
	}
	
	private void setHourHandColor(float angle){

		float h = angle > 0.0f ? Helper.Map(angle, 1.0f, MAX_ANGLE_STEP, 120f, 0f) : 120f;
		
		Color hourHandColor = Helper.ColorFromHSV(h, 1f, 1f);
		magnifier.renderer.material.color = hourHandColor;
	}

	void HandleLeapInputPointingFingerLost ()
	{
		
		//reset to default material
		magnifier.renderer.material = defaultMaterial;
		
		Invoke("ResetClock", secondsUntilReset);
		
	}

	void HandleLeapInputPointingFingerFound (Leap.Pointable p)
	{
		
		Debug.Log ("Finger found");
		
		//change material to show possiblity of interaction
		magnifier.renderer.material = interactionMaterial;
		
		//init position variables
		previousPos = CenterToFingerTip(p.StabilizedTipPosition.ToUnityTranslated());
		CancelInvoke("ResetClock");
		
	}
	
	
	
	
	private Vector2 CenterToFingerTip(Vector3 fingerTip){
		
		Vector2 fingerTip2D = new Vector2(fingerTip.x, fingerTip.y);
		fingerTip2D -= center;
		
		return fingerTip2D.normalized;
	}
	
	private float CalculateHourHandRotation(){
		int hour = DateTime.Now.Hour % 12;
		float angleHourHand = hour * (360/12);
		angleHourHand += DateTime.Now.Minute * (360.0f/(12 * 60));
		return -angleHourHand;

	}
	
	private float CalculateMinuteHandRotation(){
		float angleMinuteHand = DateTime.Now.Minute * (360/60);
		angleMinuteHand += DateTime.Now.Second * (360.0f/(60*60));
		return -angleMinuteHand;
	}
	
	private float CalculateSecondHandRotation(){
		return -DateTime.Now.Second * (360/60);	
	}
	
	
	
}
