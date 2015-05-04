using UnityEngine;
using System.Collections;
using InControl;

public class Pulse : MonoBehaviour {

	public bool specialReady = false;

	public Color myColor;
	public Color pulseColor;
	Color currentColor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//check if specialbar is full
		if (specialReady == true) {
			SpecialReady ();
		} else {
			//reset color
			this.gameObject.GetComponent<GUITexture> ().color = myColor;
		}
	}

	public void SpecialReady () {
		//plusing action
		//change color
		this.gameObject.GetComponent<GUITexture> ().color = pulseColor;
		if (InputManager.ActiveDevice.LeftTrigger.IsPressed && InputManager.ActiveDevice.LeftBumper.IsPressed
		    && InputManager.ActiveDevice.RightBumper.IsPressed && InputManager.ActiveDevice.RightTrigger.IsPressed) {
			//play sound effect
			print ("special used");
				}
		//StartCoroutine ("Pulsing");
		print ("pulsing");
	}

	//ignore
	/*IEnumerator Pulsing () {
		yield return new WaitForSeconds(0.3f);
		if (currentColor == myColor) {
			this.gameObject.GetComponent<GUITexture> ().color = pulseColor;
			StopCoroutine("Pulsing");
		} else if (currentColor == pulseColor) {
			this.gameObject.GetComponent<GUITexture> ().color = myColor;
			StopCoroutine("Pulsing");
		}
	}*/
	
}
