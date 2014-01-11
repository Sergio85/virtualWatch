using UnityEngine;
using System;
using System.Collections;

public class StartTicTac : MonoBehaviour {
	
	void Start () {		
		int secondsUntilFullHour = (59 - DateTime.Now.Minute)*60 + (60 - DateTime.Now.Second);
		Invoke("StartGongCoroutine", secondsUntilFullHour);
		
		AudioManager.Instance.Play(Soundname.tictac,true);
	}
	
	void StartGongCoroutine(){
		StartCoroutine("Gong");
	}
	
	IEnumerator Gong(){
		
		int currentHour = DateTime.Now.Hour;
		int gongs = currentHour;
		
		if(currentHour > 12){
			gongs = currentHour - 12;
		}
		
		if(currentHour == 0){
			gongs = 12;
		}
		
		AudioManager.Instance.PlayAndRepeat(gongs);
		yield return new WaitForSeconds(3600);
	}
}
