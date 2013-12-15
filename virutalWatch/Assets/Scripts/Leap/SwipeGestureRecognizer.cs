using UnityEngine;
using System.Collections;
using Leap;

public class SwipeGestureRecognizer {	
	
	enum Direction { LEFT, RIGHT, TOP, BOTTOM }; 
	
	
	private CustomSwipeGesture currentSwipe = new CustomSwipeGesture();
	
	public CustomSwipeGesture CurrentSwipe { 
		
		get{
			return currentSwipe;
		}
	
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	public Gesture.GestureState Update(Frame frame){

		
		if(currentSwipe.IsValid && currentSwipe.State != Gesture.GestureState.STATESTOP){
			currentSwipe = new CustomSwipeGesture(frame.Gesture(currentSwipe.Id));
			
			if(currentSwipe.State == Gesture.GestureState.STATEUPDATE){
				return Gesture.GestureState.STATEUPDATE;
				
			}
			else if(currentSwipe.State == Gesture.GestureState.STATESTOP){
				return Gesture.GestureState.STATESTOP;
			}
			
		}
		
		else{
			foreach(Gesture g in frame.Gestures()){
				if(g.Type == Gesture.GestureType.TYPESWIPE && g.IsValid){
					currentSwipe = new CustomSwipeGesture(g);
					return Gesture.GestureState.STATESTART;
				}
			}
			
		}
		
		return Gesture.GestureState.STATEINVALID;
		
		
	}
	

}
