using UnityEngine;
using System.Collections;

public class Lupe : MonoBehaviour {

	public GameObject lupe;
	public float offsetY;
	public float offsetX;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lupe.transform.rotation = Quaternion.Euler(90, 0, 0);
		Vector3 lupenOffset = new Vector3 (0, 0, 0);
		transform.position = lupe.transform.position;
		transform.position += lupenOffset;

		if (offsetX != null) {
				Vector3 temp = new Vector3(offsetX,0,0);
				transform.position += temp; 
		}

		if (offsetY != null) {
			Vector3 temp = new Vector3(0,offsetY,0);
			transform.position += temp; 
		}


		
	}
}
