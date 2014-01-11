using UnityEngine;
using System.Collections;

public class rotatingSky : MonoBehaviour {
	
	public mset.Sky targetSky = null;
	
	public Vector3 bigScale = new Vector3(1.2f,1.21f,1.2f);
	public Vector3 hoverScale = new Vector3(1f,1f,1f);
	public Vector3 littleScale = new Vector3(0.75f,0.76f,0.75f);
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate(0,0001f*Time.deltaTime,0);
		//transform.Rotate(0001f*Time.deltaTime,0,0);
		transform.localScale = bigScale;
		
	}
}
