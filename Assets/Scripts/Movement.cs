using UnityEngine;
using System.Collections;
using InControl;

public class Movement : MonoBehaviour {
	
	public float moveSpeed = 10;
	
	// Update is called once per frame
	void Update () {
		InputDevice inputDevice = InputManager.ActiveDevice;
		
		Vector2 stickInput = new Vector2(inputDevice.RightStickX, inputDevice.RightStickY);
		//Deadzone calc
		float deadzone = 0.20f;
		if (stickInput.magnitude < deadzone) {
			stickInput = Vector2.zero;
		} else {
			stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));
		}
		//stickInput is now adjusted with a deadzone
		
		float magnitude = stickInput.magnitude; //save the magnitude
		float angle = Mathf.Atan2 (stickInput.y, stickInput.x) * Mathf.Rad2Deg; //And save the direction
		Vector2 movement = new Vector2 (magnitude * Mathf.Cos (angle), magnitude * Mathf.Sin (angle)); //Convert polar to cartesian coordinates
		this.rigidbody2D.velocity = movement * moveSpeed; //Set the velocity to the new direction times the desired movement speed
		
		//		//Alternatively apply force instead of setting the velocity  if the player should have inertia
		//		if (this.rigidbody2D.velocity.magnitude < moveSpeed) {
		//				this.rigidbody2D.AddForce (movement);
		//		}
		
	}
}
