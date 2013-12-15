using UnityEngine;
using System.Collections;

public class FlipCard1 : MonoBehaviour {

    public float normalAngle;
    public string bottomSign;
    public Texture2D mainTexture;
    public float rotation = 0;

    public void initialArrange()
    {
        Transform inside = transform.GetChild(0);
        inside.localPosition = new Vector3(0, 3.5f, 0);
        inside.localEulerAngles = new Vector3(0, rotation, 0);

        inside.renderer.material.mainTexture = mainTexture;
    }

}
