using UnityEngine;
using System.Collections;

public class Lupe : MonoBehaviour {

	public GameObject lupe;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//lupe.transform.rotation = Quaternion.Euler(90, 0, 0);
		transform.position = lupe.transform.position;
	}
}
