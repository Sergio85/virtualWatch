using UnityEngine;
using System.Collections;

public class GearAnimation : MonoBehaviour {
	
	public const string ENDLESS_LOOP = "endlessLoop";
	
	public float anglePerStep = 1.0f;
	public double animationTime = 0.075;
	public float delay = 0.0f;
	public string easeType = "easeInSine";
	public string loopType = ENDLESS_LOOP;
	
	
	// Use this for initialization
	void Start () {
		string itweenLoopType = loopType == ENDLESS_LOOP ? "loop" : loopType;
		iTween.RotateAdd(this.gameObject, iTween.Hash("isLocal", true, "z", anglePerStep, "easetype", easeType, "time", animationTime, "delay", delay, "looptype", itweenLoopType, "oncomplete", "onComplete"));
		
	
		
	}
	
	private void onComplete(){
		if(loopType == ENDLESS_LOOP){
			this.gameObject.transform.Rotate(Vector3.forward, anglePerStep);
		}
	}
}
