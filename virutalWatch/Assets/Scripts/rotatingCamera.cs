using UnityEngine;
using System.Collections;

public class rotatingCamera : MonoBehaviour {
	
	
	public GameObject target = null;
	public bool orbit = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(target != null) {
			
			transform.LookAt(target.transform);
			
			if(orbit) {
				transform.RotateAround(target.transform.position, Vector3.down, Time.deltaTime * 15);	
			}
			
		}
	}
}
