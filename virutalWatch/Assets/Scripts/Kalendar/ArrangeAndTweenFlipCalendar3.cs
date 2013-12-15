using UnityEngine;
using System.Collections;
using System;

public class ArrangeAndTweenFlipCalendar3 : MonoBehaviour {

    private float divider = 360;
    Boolean notBlocked = true;
    private float[] angles;
    public string selectedKey = "5";
    public float delayBeforeFlip = 0.1f;


    public int selectedGOindex = 1;

    public GameObject[] Childs;
    public float radius;
    public float openingAngle = 178;
    public float timeForOneFlip = 0.2f;

	// Use this for initialization
	void Start () {
        divider = divider / Childs.Length;
        float normalAngle = divider;
        
        float multiplyer = openingAngle / (Childs.Length);
        float multiplyerState = (openingAngle / 2);

        angles = new float[Childs.Length];

        int i = 0;
        foreach(GameObject Child in Childs){
            // arrange childs an side of celiner
            ((FlipCard1)Child.GetComponent("FlipCard1")).initialArrange();


            Child.transform.localEulerAngles = new Vector3(0, 0, normalAngle);
            ((FlipCard1)Child.GetComponent("FlipCard1")).normalAngle = Child.transform.localEulerAngles.z;

            
            Child.transform.Translate(0, radius , 0);

            normalAngle += divider;
            if(normalAngle >= 360){
                normalAngle -= 360;
            }

            // Fill Angles in Array
            angles[i] = multiplyerState;

            //Debug.Log(multiplyerState);
            multiplyerState -= multiplyer;
            i++;
        }
        
	}
	
	// Update is called once per frame
    void Update()
    {

		/*if (Input.GetKey(KeyCode.Keypad1) && notBlocked)
        {
            selectedKey = "P";
            flipTo();
        }
        if (Input.GetKey(KeyCode.Keypad0) && notBlocked)
        {
            selectedKey = "A";
            flipTo();
        }

        if(Input.GetKey("up")){
            flipTo();
        }*/
    }

    public void flipTo()
    {

        float random = UnityEngine.Random.value / 10;

        //Debug.Log(((FlipCard)Childs[Childs.Length - selectedGOindex - 1].GetComponent("FlipCard")).bottomSign);
        int goOnFrontIndex = Childs.Length - selectedGOindex - 1;
        float timeMultiplyer = 1;
        
        int j = 0;
        int roundsToTake = 0;
        foreach(GameObject Child in Childs){

            if (((FlipCard1)Childs[j].GetComponent("FlipCard1")).bottomSign.Equals(selectedKey))
            {
                if (goOnFrontIndex > j)

                    roundsToTake = goOnFrontIndex - j;
                else
                    roundsToTake = (Childs.Length - j) + goOnFrontIndex;
            }

            j++;
        }

        //Debug.Log("GO On Front: " + goOnFrontIndex );//+ "   Rounds To Take: " + roundsToTake + "   Selected GO Index: " + selectedGOindex);
        
        if(roundsToTake == 1)
        {
            timeMultiplyer = 8;
        } 
        else if (roundsToTake == 2)
        {
            timeMultiplyer = 4;
        }
        else if (roundsToTake == 3)
        {
            timeMultiplyer = 2.3f;
        } 
        else if (roundsToTake == 4)
        {
            timeMultiplyer = 1.5f;
        }
        else
        {
            timeMultiplyer = 1;
        }

        
        
        
        if (!((FlipCard1)Childs[goOnFrontIndex].GetComponent("FlipCard1")).bottomSign.Equals(selectedKey))
        {

            // Rotate the whole Object
            //iTween.RotateBy(gameObject, iTween.Hash("z", 1.0f / Childs.Length, "easeType", "easeInOutQuint", "time", timeForOneFlip * timeMultiplyer + delayBeforeFlip, "isLocal", true, "onComplete", "flipTo"));
            iTween.RotateBy(gameObject, iTween.Hash("z", 1.0f / Childs.Length, "easeType", "easeInOutQuint", "time", timeForOneFlip * timeMultiplyer + random, "isLocal", true, "onComplete", "flipTo"));
            selectedGOindex++;
            if (selectedGOindex == Childs.Length)
            {
                selectedGOindex = 0;
            }

            int i = selectedGOindex;
            foreach (GameObject Child in Childs)
            {

                if (Childs[goOnFrontIndex] == Child)
                {
                    iTween.RotateTo(Child, iTween.Hash("z", ((FlipCard1)Child.GetComponent("FlipCard1")).normalAngle + angles[i % Childs.Length], "easeType", "easeOutBounce", "time", timeForOneFlip * timeMultiplyer + random, "isLocal", true));//, "delay", delayBeforeFlip * timeMultiplyer));
                }
                else
                {
                    iTween.RotateTo(Child, iTween.Hash("z", ((FlipCard1)Child.GetComponent("FlipCard1")).normalAngle + angles[i % Childs.Length], "easeType", "easeInOutQuint", "time", timeForOneFlip * timeMultiplyer + random, "isLocal", true));//, "delay", delayBeforeFlip * timeMultiplyer));
                }
                i++;

            }

            notBlocked = false;
            

        }
        else // end of animation
        {
            notBlocked = true;
        }
        
    }

    public bool animationIsRunning()
    {
        return !notBlocked;
    }

}
