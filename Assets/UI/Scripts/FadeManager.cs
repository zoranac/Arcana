using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour {

	public bool activeState = false;

	//public Color myColor;
	public Color hide;

	//float fadeSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//myColor = Color.Lerp(Color.black, Color.white, fadeSpeed);
		//this.gameObject.GetComponent<GUIText> ().color = myColor;

		//check for ability state
		if (activeState == false) {
			Active ();
		} else if(activeState == true) {
			Inactive();
		}
	}

	//set inactive color and begin fade into active
	void Inactive () {
		//placehold change
		//guiTexture.color = hide;
	}

	//set active color
	void Active () {
		//placehold change
		guiTexture.color = Color.white;
	}
}
