using UnityEngine;
using System.Collections;

public class CalendarController : MonoBehaviour {

    public GameObject[] Elements;

    public string text = "TEST12";

    bool blocked = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Keypad1))
        {
            AnimateTo(text);
        }
	}

    public void AnimateTo(string sixAlphaNumForCalendar)
    {
        text = sixAlphaNumForCalendar;

        int i = 0;
        foreach (GameObject element in Elements)
        {
            ArrangeAndTweenFlipCalendar3 part = ((ArrangeAndTweenFlipCalendar3)element.GetComponent("ArrangeAndTweenFlipCalendar3"));
			Debug.Log( text.Substring(i,1));
            part.selectedKey = text.Substring(i, 1);
            if (!part.animationIsRunning())
                part.flipTo();

            i++;
        }
    }
}
