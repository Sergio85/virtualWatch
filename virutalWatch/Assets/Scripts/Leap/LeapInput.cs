
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;

/// <summary>
/// This static class serves as a static wrapper to provide some helpful C# functionality.
/// The main use is simply to provide the most recently grabbed frame as a singleton.
/// Events on aquiring, moving or loosing hands are also provided.  If you want to do any
/// global processing of data or input event dispatching, add the functionality here.
/// It also stores leap input settings such as how you want to interpret data.
/// To use it, you must call Update from your game's main loop.  It is not fully thread safe
/// so take care when using it in a multithreaded environment.
/// </summary>
public static class LeapInput 
{	
	/// <summary>
	/// Delegates for the events to be dispatched.  
	/// </summary>
	public delegate void PointableFoundHandler( Pointable p );
	public delegate void PointableUpdatedHandler( Pointable p );
	public delegate void HandFoundHandler( Hand h );
	public delegate void HandUpdatedHandler( Hand h );
	public delegate void ObjectLostHandler( int id );
	
	public delegate void InteractionFoundHandler();
	public delegate void InteractionLostHandler();
	
	public delegate void FingerPointingHandler(Pointable p);
	public delegate void FingerUpdatedHandler(Pointable p);
	public delegate void FingerLostHandler();
	
	
	public delegate void SwipeGestureStartHandler(CustomSwipeGesture g);
	public delegate void SwipeGestureUpdateHandler(CustomSwipeGesture g);
	public delegate void SwipeGestureEndHandler(CustomSwipeGesture g);
	
	/// <summary>
	/// Event delegates are trigged every frame in the following order:
	/// Hand Found, Pointable Found, Hand Updated, Pointable Updated,
	/// Hand Lost, Hand Found.
	/// </summary>
	public static event PointableFoundHandler PointableFound;
	public static event PointableUpdatedHandler PointableUpdated;
	public static event ObjectLostHandler PointableLost;
	
	public static event HandFoundHandler HandFound;
	public static event HandUpdatedHandler HandUpdated;
	public static event ObjectLostHandler HandLost;
	
	public static event FingerPointingHandler PointingFingerFound;
	public static event FingerUpdatedHandler PointingFingerUpdated;
	public static event FingerLostHandler PointingFingerLost;
	
	public static event SwipeGestureStartHandler HorizontalSwipeStart;
	public static event SwipeGestureStartHandler VerticalSwipeStart;
	public static event SwipeGestureUpdateHandler HorizontalSwipeUpdate;
	public static event SwipeGestureUpdateHandler VerticalSwipeUpdate;
	public static event SwipeGestureEndHandler HorizontalSwipeEnd;
	public static event SwipeGestureEndHandler VerticalSwipeEnd;
	
	public static event InteractionFoundHandler InteractionStart;
	public static event InteractionLostHandler InteractionEnd;
	
	public static Leap.Frame Frame
	{
		get { return m_Frame; }
	}
	
	public static void Start(){
		m_controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
		m_controller.Config.SetFloat("MinVelocity", 500f);
		m_controller.Config.Save();
	}
	
	public static void Update() 
	{	
		if( m_controller != null )
		{
			
			Frame lastFrame = m_Frame == null ? Frame.Invalid : m_Frame;
			m_Frame	= m_controller.Frame();
			
			
			DispatchLostEvents(Frame, lastFrame);
			DispatchFoundEvents(Frame, lastFrame);
			DispatchUpdatedEvents(Frame, lastFrame);
			bool checkForSwipe = !RecognizePointingGesture();
			
			if(checkForSwipe){
				RecognizeSwipeGesture();
			}
			
			
		}
	}
	
	//*********************************************************************
	// Private data & functions
	//*********************************************************************
	private enum HandID : int
	{
		Primary		= 0,
		Secondary	= 1
	};
	
	//Private variables
	static Leap.Controller 		m_controller	= new Leap.Controller();
	
	static Leap.Frame			m_Frame			= null;
	static PointingGestureRecognizer pointingGestureRecognizer = new PointingGestureRecognizer();
	static SwipeGestureRecognizer swipeGestureRecoginizer = new SwipeGestureRecognizer();
	
	
	private static void RecognizeSwipeGesture(){
		Gesture.GestureState state = swipeGestureRecoginizer.Update(Frame);
		CustomSwipeGesture gesture = swipeGestureRecoginizer.CurrentSwipe;
		if(HorizontalSwipeStart!= null && state == Gesture.GestureState.STATESTART && gesture.isHorizontal())
			HorizontalSwipeStart(gesture);
		else if(VerticalSwipeStart!= null && state == Gesture.GestureState.STATESTART && gesture.isVertical())
			VerticalSwipeStart(gesture);
		else if(HorizontalSwipeUpdate != null && state == Gesture.GestureState.STATEUPDATE && gesture.isHorizontal())
			HorizontalSwipeUpdate(gesture);
		else if(VerticalSwipeUpdate != null && state == Gesture.GestureState.STATEUPDATE && gesture.isVertical())
			VerticalSwipeUpdate(gesture);
		else if(HorizontalSwipeEnd != null && state == Gesture.GestureState.STATESTOP && gesture.isHorizontal())
			HorizontalSwipeEnd(gesture);
		else if(VerticalSwipeEnd != null && state == Gesture.GestureState.STATESTOP && gesture.isVertical())
			VerticalSwipeEnd(gesture);
		
	}
	
	
	private static bool RecognizePointingGesture(){
		Gesture.GestureState pointingGestureState = pointingGestureRecognizer.Update(Frame);
		switch(pointingGestureState){
		case Gesture.GestureState.STATESTART:
			if(PointingFingerFound != null){
				PointingFingerFound(pointingGestureRecognizer.TrackedPointable);
				return true;
			}
			break;
		case Gesture.GestureState.STATEUPDATE:
			if(PointingFingerUpdated != null){
				PointingFingerUpdated(pointingGestureRecognizer.TrackedPointable);
				return true;
			}
			break;
		case Gesture.GestureState.STATESTOP:
			if(PointingFingerLost != null){
				PointingFingerLost();
				return true;
			}
			break;
		}
		
		return false;
	}
	
	private static void DispatchLostEvents(Frame newFrame, Frame oldFrame)
	{
		
		if(newFrame.Hands.Empty && !oldFrame.Hands.Empty){
			if(InteractionEnd != null)
				InteractionEnd();
		}
		
		foreach( Hand h in oldFrame.Hands )
		{
			if( !h.IsValid )
				continue;
			if( !newFrame.Hand(h.Id).IsValid && HandLost != null )
				HandLost(h.Id);
		}
		foreach( Pointable p in oldFrame.Pointables )
		{
			if( !p.IsValid )
				continue;
			if( !newFrame.Pointable(p.Id).IsValid && PointableLost != null )
				PointableLost(p.Id);
		}
		
		
	}
	private static void DispatchFoundEvents(Frame newFrame, Frame oldFrame)
	{
		
		if(!newFrame.Hands.Empty && oldFrame.Hands.Empty){
			if(InteractionStart != null)
				InteractionStart();
		}
		
		foreach( Hand h in newFrame.Hands )
		{
			if( !h.IsValid )
				continue;
			if( !oldFrame.Hand(h.Id).IsValid && HandFound != null)
				HandFound(h);
		}
		foreach( Pointable p in newFrame.Pointables )
		{
			if( !p.IsValid )
				continue;
			if( !oldFrame.Pointable(p.Id).IsValid && PointableFound != null ){
				PointableFound(p);
			}
				
		}
		

		
		
	}
	private static void DispatchUpdatedEvents(Frame newFrame, Frame oldFrame)
	{
		foreach( Hand h in newFrame.Hands )
		{
			if( !h.IsValid )
				continue;
			if( oldFrame.Hand(h.Id).IsValid && HandUpdated != null)
				HandUpdated(h);
		}
		foreach( Pointable p in newFrame.Pointables )
		{
			if( !p.IsValid )
				continue;
			if( oldFrame.Pointable(p.Id).IsValid && PointableUpdated != null)
				PointableUpdated(p);
		}
		

		
	}
}
