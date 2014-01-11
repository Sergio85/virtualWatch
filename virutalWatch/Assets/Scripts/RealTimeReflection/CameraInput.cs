using UnityEngine;
using System.Collections;

public class CameraInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WebCamTexture wct = new WebCamTexture ();
		renderer.material.mainTexture = wct;
		wct.Play ();
	}
	

}
