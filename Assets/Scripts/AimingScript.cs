using UnityEngine;
using System.Collections;

public class AimingScript : MonoBehaviour {

	public float rotationSpeed = 20f; //Speed to rotate the wand
	
	// Update is called once per frame
	void Update () {
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed; //Getaxis times rotation speed
		rotation *= Time.deltaTime; //Divided by delta time, equals how much the wand should rotate
		this.transform.Rotate (0, 0, rotation); //Rotate the wand
	}
}