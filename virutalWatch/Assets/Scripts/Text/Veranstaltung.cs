using UnityEngine;
using System.Collections;

public class Veranstaltung {

	
	public Veranstaltung(GUIContent f, GUIContent r, GUIContent z) {
		this.fachName = f;
		this.raum = r;
		this.zeit = z;
	}
	
	private GUIContent fachName, raum, zeit;	
	
	
	public void setFachName(string s) {
		this.fachName = new GUIContent(s);
	}
	
	public GUIContent getFachName() {
		return this.fachName;	
	}
	
	
	public void setRaum(string s) {
		this.raum = new GUIContent(s);
	}
	
	public GUIContent getRaum() {
		return this.raum;	
	}
	
	public void setZeit(string s) {
		this.zeit = new GUIContent(s);
	}
	
	public GUIContent getZeit() {
		return this.zeit;	
	}
	
	

}
