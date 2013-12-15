using UnityEngine;
using System.Collections;

public class UntweenedGearAnimation : MonoBehaviour {

	public const float timeSpread = 10f;
	public float anglePerSecond = 0.0f;
	public bool constantTime = true;
	
	// Use this for initialization
	void Start () {
		if (!constantTime) {
			anglePerSecond *= timeSpread;
		}

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(Vector3.forward, anglePerSecond * Time.deltaTime);
	}
}
