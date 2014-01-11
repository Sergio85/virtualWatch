using UnityEngine;
using System.Collections;

public class Ziffernblatt_fadeOut : MonoBehaviour {


	public GameObject ziffernBlatt;
	public GameObject striche_alle;
	public GameObject ziffern_alle;
	public GameObject fonts;
	
	public float fadeTime = 1f;
	
	private bool isVisible;
	private Color colorZiffernblatt;
	Color color_striche;
	Color color_ziffern;
	private Color color_backup;


	// Use this for initialization
	void Start () {
		colorZiffernblatt = ziffernBlatt.renderer.material.color;
		color_striche = striche_alle.renderer.material.color;
		color_ziffern = ziffern_alle.renderer.material.color;

		color_backup = colorZiffernblatt;
		isVisible = true;
		
		LeapInput.VerticalSwipeStart += OnVerticalSwipeStart;
		
		
	}
	
	private void OnVerticalSwipeStart(CustomSwipeGesture swipeGesture){
		if(swipeGesture.SwipeDirection == Interaction.Direction.TOP){
			FadeOut();
		}
		else if(swipeGesture.SwipeDirection == Interaction.Direction.BOTTOM){
			FadeIn();
		}
	}
	
	private void FadeOut(){
		if(isVisible){
			
			AudioManager.Instance.Play(Soundname.wipein, false);
			
			colorZiffernblatt.a = 0.1f;
			iTween.ColorTo(ziffernBlatt, colorZiffernblatt, fadeTime);
	
			//striche	
			color_striche.a = 0.2f;
			iTween.ColorTo(striche_alle, color_striche, fadeTime);
	
			// ziffern
			color_ziffern.a = 0.2f;
			iTween.ColorTo(ziffern_alle, color_ziffern, fadeTime);
	
	
			isVisible = false;
		}
	}
	
	private void FadeIn(){
		if(!isVisible){
			AudioManager.Instance.Play(Soundname.wipeout, false);
			
			colorZiffernblatt.a = 1;
			iTween.ColorTo(ziffernBlatt, color_backup, fadeTime);
	
			//striche
			color_striche.a = 1;
			iTween.ColorTo(striche_alle, color_striche, fadeTime);
			
			// ziffern
			color_ziffern.a = 1;
			iTween.ColorTo(ziffern_alle, color_ziffern, fadeTime);
	
	
			isVisible = true;
		}
	}
	
	
	
}
