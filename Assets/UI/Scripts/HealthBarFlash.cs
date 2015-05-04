using UnityEngine;
using System.Collections;

public class HealthBarFlash : MonoBehaviour {

	public Color myColor;
	public Color flashColor;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//debug damage input
		//if (Input.GetKeyDown (KeyCode.D))
		//	FlashBar ();
	}

	//damage flash
	public void FlashBar () {
		this.gameObject.GetComponent<GUITexture> ().color = flashColor;
		StartCoroutine ("Flash");
	}

	//health bar damage flash
	IEnumerator Flash () {
		yield return new WaitForSeconds(0.1f);
		this.gameObject.GetComponent<GUITexture> ().color = myColor;
	}
}
