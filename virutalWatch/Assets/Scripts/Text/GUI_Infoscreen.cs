using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Infoscreen : MonoBehaviour {

	public GameObject glasscheibe_info;
	
	public GUIStyle ueberschriftStyle;
	public GUIStyle fachStyle;
	public GUIStyle restStyle;
	
	public int secondsUntilReset = 10;
	
	
	public int margin_ueberschrift_;
	private int margin_fach_ = 200;
	public int margin_raumzeit_ = 150;
	public int margin_newVL = 150;
	public int margin_start_ = 20;
	
	private GUIContent ueberschrift;
	private GUIContent fachName, raum, zeit;
	private GUIContent titel, text;
	private List<Veranstaltung> VeranstalungListe = new List<Veranstaltung>();
	private List<News> NewsListe = new List<News>();

	private Rect areaRectVL = new Rect(0,4000, Screen.width, Screen.height);
	private Rect areaRectNews = new Rect(2000,2000, Screen.width, Screen.height);
	
	private bool infoActive = false;
	private bool glassActive = false;
	
	private bool isVisible = false;
	
	
	
	// Use this for initialization
	void Start () {
		
		LeapInput.InteractionStart += OnInteractionStart;
		LeapInput.InteractionEnd += OnInteractionEnd;
		
		// Veranstaltungen	
		fachName = new GUIContent("_Computer Animation");
		raum = new GUIContent("S1.5");
		zeit = new GUIContent("14:00 Uhr");
		VeranstalungListe.Add(new Veranstaltung(fachName, raum, zeit));
		
		fachName = new GUIContent("_VR/AR");
		raum = new GUIContent("H1.5");
		zeit = new GUIContent("09:00 Uhr");
		VeranstalungListe.Add(new Veranstaltung(fachName, raum, zeit));
		
		fachName = new GUIContent("_Mathematik");
		raum = new GUIContent("H1.5");
		zeit = new GUIContent("09:00 Uhr");
		VeranstalungListe.Add(new Veranstaltung(fachName, raum, zeit));
		
		
		
		// News
		titel = new GUIContent("_Textanimation in Unity");
		text = new GUIContent("Lorem ipsum dolor sit amet..");
		NewsListe.Add(new News(titel, text));
		
		/////////////////////////////////////////////////////////////////////////
		
		infoActive = true;
		
	}
	
	void OnInteractionStart(){
		if(!isVisible){
			slideUp();
		}
		else{
			CancelInvoke("slideDown");
		}
	}
	
	void OnInteractionEnd(){
		Invoke("slideDown", secondsUntilReset);
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( Input.GetKey(KeyCode.UpArrow) ) {
	
		}
		if ( Input.GetKey(KeyCode.RightArrow) ) {
			slideUp();
		}	
		if ( Input.GetKey(KeyCode.LeftArrow) ) {
			slideDown();
		}
		
		
	}

	void slideGlass() {


		//MoveTo(GameObject target, Vector3 position, float time)

	}
	
	void OnGUI() {
		
		
		if(infoActive) {


			// VORLESUNGEN //////////////////////////////////////////////////////////////////////////////////
			GUILayout.BeginArea(areaRectVL);
			
			ueberschrift = new GUIContent("VORLESUNGEN");
			GUI.Box (new Rect (180,margin_start_,300,40), ueberschrift, ueberschriftStyle);
			
			
			for(int i = 0; i<VeranstalungListe.Count; i++) {
				
				GUI.Box (new Rect (180,margin_fach_,150,40), VeranstalungListe[i].getFachName(), fachStyle);
				GUI.Box (new Rect (180,margin_raumzeit_,50,20), VeranstalungListe[i].getZeit(), restStyle);
				GUI.Box (new Rect (330,margin_raumzeit_,150,20), VeranstalungListe[i].getRaum(), restStyle);
				
				margin_fach_ += 100;
				margin_raumzeit_ += 100;
	
			}
			
			
			GUILayout.EndArea();
			
			margin_fach_ = 200;
			margin_raumzeit_ = 150;
			
			///////////////////////////////////////////////////////////////////////////////////////////////////
			
			
			// NEWS //////////////////////////////////////////////////////////////////////////////////
			GUILayout.BeginArea(areaRectNews);
			
			ueberschrift = new GUIContent("NEWS");
			GUI.Box (new Rect (180,margin_start_,300,40), ueberschrift, ueberschriftStyle);
			
			
			for(int i = 0; i<NewsListe.Count; i++) {
				
				GUI.Box (new Rect (180,margin_fach_,150,40), NewsListe[i].getTitel(), fachStyle);
				GUI.Box (new Rect (180,margin_raumzeit_,300,20), NewsListe[i].getText(), restStyle);
			
				
				margin_fach_ += 50;
				margin_raumzeit_ += 50;
	
			}
			
			
			GUILayout.EndArea();

			
			margin_fach_ = 80;
			margin_raumzeit_ = 110;
			
			///////////////////////////////////////////////////////////////////////////////////////////////////


		}
	}
	
	void slideUp() {
		
		AudioManager.Instance.Play(Soundname.wipein, false);
		
		isVisible = true;
		Vector3 glassOben = new Vector3 (0f, 8f, -18f);	
		iTween.MoveTo (glasscheibe_info, glassOben, 1f);

		iTween.ValueTo(gameObject, iTween.Hash("from", areaRectVL, "to", inScreenUp(areaRectVL), "delay", 0.3f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRectVL"));

	}

	void slideDown() {
		
		AudioManager.Instance.Play(Soundname.wipeout, false);
		
		isVisible = false;
		Vector3 glassUnten = new Vector3 (0f, -68f, -18f);	
		iTween.MoveTo (glasscheibe_info, glassUnten, 1f);
		
		iTween.ValueTo(gameObject, iTween.Hash("from", areaRectVL, "to", offScreenDown(areaRectVL), "delay", 0f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRectVL"));
		
	}
	
	
	void slideLeft() {			
		iTween.ValueTo(gameObject, iTween.Hash("from", areaRectVL, "to", offScreenLeft(areaRectVL), "delay", 0f, "time", 1.25f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRectVL"));
		iTween.ValueTo(gameObject, iTween.Hash("from", areaRectNews, "to", inScreenRight(areaRectNews), "delay", 0f, "time", 1.25f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRectNews"));
	}
	
	
	void slideRight() {			
		iTween.ValueTo(gameObject, iTween.Hash("from", areaRectVL, "to", inScreenRight(areaRectVL), "delay", 0f, "time", 1.25f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRectVL"));
		iTween.ValueTo(gameObject, iTween.Hash("from", areaRectNews, "to", offScreenRight(areaRectNews), "delay", 0.5f, "time", 1.25f, "easetype", iTween.EaseType.easeInOutSine, "onupdate", "updateRectNews"));
	}
	
	
	
	// Dat kann man auch schöner lösen....	
	Rect offScreenLeft ( Rect input ) {
        return new Rect(new Rect(-5000,2000, Screen.width, Screen.height));
	}
	
	Rect offScreenRight ( Rect input ) {
        return new Rect(new Rect(5000,2000, Screen.width, Screen.height));
	}
	
	Rect inScreenRight ( Rect input) {
		return new Rect(new Rect(0,2000, Screen.width, Screen.height));
	}

	Rect inScreenUp ( Rect input) {
		return new Rect(new Rect(0,2000, Screen.width, Screen.height));
	}

	Rect offScreenDown ( Rect input) {
		return new Rect(new Rect(0,4000, Screen.width, Screen.height));
	}


	void updateRectVL ( Rect input )	{
		areaRectVL = input;
	}
	void updateRectNews ( Rect input )	{
		areaRectNews = input;
	}
	
}






