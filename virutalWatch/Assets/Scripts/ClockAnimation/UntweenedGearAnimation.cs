using UnityEngine;
using System.Collections;

public class UntweenedGearAnimation : MonoBehaviour {

	public float anglePerSecond = 0.0f;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(Vector3.forward, anglePerSecond * Time.deltaTime);
	}
}
