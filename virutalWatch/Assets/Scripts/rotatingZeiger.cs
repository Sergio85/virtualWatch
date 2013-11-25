using UnityEngine;
using System.Collections;

public class rotatingZeiger : MonoBehaviour {
	
	public GameObject lupenCam = null;
	public GameObject lupe = null;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,010f*Time.deltaTime,0);
	
		if(lupenCam != null && lupe != null) {
			
			lupenCam.transform.position = lupe.transform.position;
			
		}
		
		
	}
}
