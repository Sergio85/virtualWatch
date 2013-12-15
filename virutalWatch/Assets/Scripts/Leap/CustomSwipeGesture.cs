using System;
using Leap;
using Interaction;
public class CustomSwipeGesture : SwipeGesture
{
	
	public Interaction.Direction SwipeDirection { get; set; }
	
	
	public CustomSwipeGesture(){}
	
	public CustomSwipeGesture (Gesture g) : base(g)
	{
		//Horizontal Swipe
		if(Math.Abs(this.Direction.x) > Math.Abs(this.Direction.y)){
			if(this.Direction.x < 0)
				SwipeDirection = Interaction.Direction.RIGHT;
			else
				SwipeDirection = Interaction.Direction.LEFT;
		}
		else{
			if(this.Direction.y < 0)
				SwipeDirection = Interaction.Direction.TOP;
			else
				SwipeDirection = Interaction.Direction.BOTTOM;
		}
	}
	
	public bool isHorizontal(){
		return SwipeDirection == Interaction.Direction.LEFT || SwipeDirection == Interaction.Direction.RIGHT;
	}
	public bool isVertical(){
		return SwipeDirection == Interaction.Direction.TOP || SwipeDirection == Interaction.Direction.BOTTOM;
	}
}


