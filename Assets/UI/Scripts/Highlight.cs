using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

	public Color selected;
	public Color deselected;
	public Color myColor;

	public float fadeSpeed = 0;
	int fontSize;

	// Use this for initialization
	void Start () {
		fontSize = this.gameObject.GetComponent<GUIText> ().fontSize;
		myColor = deselected;
		fadeSpeed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//updates color of text
		myColor = Color.Lerp(deselected, selected, fadeSpeed);
		this.gameObject.GetComponent<GUIText> ().color = myColor;
		this.gameObject.GetComponent<GUIText> ().fontSize = fontSize;
		//debug test
		/*if (Input.GetKey (KeyCode.A))
			_Highlight ();

		if (Input.GetKey (KeyCode.B))
			Unhighlight ();
						*/
	}

	//counts up fadespeed variable to 1
	public void _Highlight () {
		//adjust color
		fadeSpeed += 1.75f * Time.deltaTime;
		if (fadeSpeed > 1)
			fadeSpeed = 1;

		//adjust font size
		if (fontSize < 56)
			fontSize += 1;

	}

	//counts down fadespeed variable to 0
	public void Unhighlight () {
		//adjust color
		fadeSpeed -= 1.75f * Time.deltaTime;
		if (fadeSpeed < 0)
			fadeSpeed = 0;

		//adjust font size
		if (fontSize > 48)
			fontSize -= 1;
	}
}
