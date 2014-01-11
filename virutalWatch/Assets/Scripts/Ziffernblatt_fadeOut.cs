using UnityEngine;
using System.Collections;

public class Ziffernblatt_fadeOut : MonoBehaviour {


	public GameObject ziffernBlatt;
	public GameObject striche_alle;
	public GameObject ziffern_alle;
	public GameObject fonts;
	private bool active;
	private Color colorZiffernblatt;
	Color color_striche;
	Color color_ziffern;
	private Color color_backup;


	// Use this for initialization
	void Start () {
		colorZiffernblatt = ziffernBlatt.renderer.material.color;
		color_striche = striche_alle.renderer.material.color;
		color_ziffern = ziffern_alle.renderer.material.color;

		color_backup = colorZiffernblatt;
		active = true;
	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetKey(KeyCode.DownArrow) ) {


			if(active) {
				if(ziffernBlatt != null && striche_alle != null && ziffern_alle != null && fonts != null) {
						
					//ziffernblatt	
						colorZiffernblatt.a = 0.1f;
						iTween.ColorTo(ziffernBlatt, colorZiffernblatt, 3f);

						//striche	
						color_striche.a = 0.2f;
						iTween.ColorTo(striche_alle, color_striche, 3f);

						// ziffern
						color_ziffern.a = 0.2f;
						iTween.ColorTo(ziffern_alle, color_ziffern, 3f);


						active = false;
					}
			}
			else {
						//ziffernblatt
						colorZiffernblatt.a = 1;
						iTween.ColorTo(ziffernBlatt, color_backup, 3f);

						//striche
						color_striche.a = 1;
						iTween.ColorTo(striche_alle, color_striche, 3f);
						
						// ziffern
						color_ziffern.a = 1;
						iTween.ColorTo(ziffern_alle, color_ziffern, 3f);


						active = true;
				}
			}
	
		}
}
