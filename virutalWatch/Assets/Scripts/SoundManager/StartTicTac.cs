using UnityEngine;
using System;
using System.Collections;

public class StartTicTac : MonoBehaviour {

	// Use this for initialization
	public float timedelay = 3.0F;
	public int minutes,time,seconds;
	public float pitch = 0.5F;
	void Start () {
		//StartCoroutine("WaitAndGong",5);
		
		int secondsUntilFullHour = (59 - DateTime.Now.Minute)*60 + (60 - DateTime.Now.Second);
		Invoke("StartGongCoroutine", secondsUntilFullHour);
		
		Debug.Log (secondsUntilFullHour);
		
		AudioManager.Instance.Play(Soundname.tictac,true);
		
	}
	
	void StartGongCoroutine(){
		StartCoroutine("Gong");
	}
	
	IEnumerator Gong(){
		AudioManager.Instance.Play(Soundname.gong,false);
		yield return new WaitForSeconds(3600);
	}
}
