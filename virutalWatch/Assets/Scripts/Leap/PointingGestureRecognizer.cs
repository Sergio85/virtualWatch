using System;
using Leap;
using UnityEngine;
public class PointingGestureRecognizer
{
	
	public const int FRAMES_TILL_DETECTION = 40;
	Pointable trackedPointable;
	int countFrames = 0;
	public const int Z_DISTANCE = 100;
	
	public PointingGestureRecognizer ()
	{}
	
	public Gesture.GestureState Update(Frame frame){
		
		if(trackedPointable != null){
			if(frame.Pointable(trackedPointable.Id).IsValid && frame.Pointable(trackedPointable.Id).TipPosition.z < Z_DISTANCE){
				trackedPointable = frame.Pointable(trackedPointable.Id);
				return Gesture.GestureState.STATEUPDATE;
				
			}
			else{
				trackedPointable = null;
				return Gesture.GestureState.STATESTOP;
			}		
			
		}
		else{
			
			//Wait for activation
			if(frame.Pointables.Count == 1){
				countFrames++;	
			}
			else{
				countFrames = 0;
			}
			
			//Trigger Start-Event if Frame-Treshold reached
			if(countFrames == FRAMES_TILL_DETECTION){
				trackedPointable = frame.Pointables[0];
				countFrames = 0;
				if(trackedPointable.TipPosition.z < Z_DISTANCE){
					return Gesture.GestureState.STATESTART;
				}
			}
			
		}
		
		return Gesture.GestureState.STATEINVALID;
	}
	
	public Pointable TrackedPointable
	{
		get
		{
			return trackedPointable;	
		}
	}
	
}

