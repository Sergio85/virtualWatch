using UnityEngine;
using System.Collections;

public class News {

	public News(GUIContent titel, GUIContent text) {
		this.titel = titel;
		this.text = text;
	}
	
	private GUIContent titel, text;	
	
	
	public void setTitel(string s) {
		this.titel = new GUIContent(s);
	}
	
	public GUIContent getTitel() {
		return this.titel;	
	}
	
	
	public void setText(string s) {
		this.text = new GUIContent(s);
	}
	
	public GUIContent getText() {
		return this.text;	
	}
	
}

