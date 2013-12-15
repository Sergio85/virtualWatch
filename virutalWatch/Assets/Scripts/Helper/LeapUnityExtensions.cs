/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2013.                                   *
* Leap Motion proprietary and  confidential.  Not for distribution.            *
* Use subject to the terms of the Leap Motion SDK Agreement available at       *
* https://developer.leapmotion.com/sdk_agreement, or another agreement between *
* Leap Motion and you, your company or other organization.                     *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;

namespace Leap {
	
	//Extension to the unity vector class. Provides automatic scaling into unity scene space.
	//Leap coordinates are in cm, so the .02f scaling factor means 1cm of hand motion = .02m scene motion
	public static class UnityVectorExtension
	{
		public static Vector3 InputScale = new Vector3(1f, 1f, 1f);
		public static Vector3 InputOffset = new Vector3(0,0,0);
		
		//For Directions
		public static Vector3 ToUnity(this Vector lv)
		{
			return Rotated(FlippedZ(lv));
		}
		//For Acceleration/Velocity
		public static Vector3 ToUnityScaled(this Vector lv)
		{
			return Scaled(Rotated(FlippedZ( lv )));
		}
		//For Positions
		public static Vector3 ToUnityTranslated(this Vector lv)
		{
			return Offset(Scaled(Rotated(FlippedZ( lv ))));
		}
		
		private static Vector3 Rotated(Vector3 v){
			Matrix4x4 transformMatrix = new Matrix4x4();
			transformMatrix.SetTRS(Vector3.zero, Quaternion.Euler(new Vector3(0, 0, 180f)), Vector3.one);
			return transformMatrix.MultiplyVector(v);
		}

		private static Vector3 Scaled( Vector3 v ) { return new Vector3( v.x * InputScale.x,
																		 v.y * InputScale.y,
																		 v.z * InputScale.z ); }
		private static Vector3 Offset( Vector3 v ) 
		{ 
			return v + InputOffset;
		}
		
		private static Vector3 FlippedZ(this Vector lv){
			return new Vector3(lv.x, lv.y, -lv.z);	
		}
		
	}
}