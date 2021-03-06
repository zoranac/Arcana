﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class SpellcastScript : MonoBehaviour {
	
	public GameObject spell; //The gameobject for the spell projectile fired
	public Transform origin; //Transform to spawn the spell shot at
	public float shotCool = 0f; //Cooldown of the fire rate

	public SpellCombo[] myCombos = new SpellCombo[]{
		new SpellCombo(),
		new SpellCombo(),
		new SpellCombo(),
		new SpellCombo()
	};
	
	public int selectedSpell = 0;

	void Start () {
		myCombos [0].InitializeValues (0, 0);
		myCombos [1].InitializeValues (0, 0);
		myCombos [2].InitializeValues (0, 0);
		myCombos [3].InitializeValues (0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		InputDevice inputDevice = InputManager.ActiveDevice;
		
		//Select what spell is active
		if (inputDevice.LeftBumper.IsPressed) {
			selectedSpell = 0;
		} else if (inputDevice.RightBumper.IsPressed) {
			selectedSpell = 1;
		} else if (inputDevice.LeftTrigger.IsPressed) {
			selectedSpell = 2;
		} else if (inputDevice.RightTrigger.IsPressed) {
			selectedSpell = 3;
		}
		
		//Deadzone adjustment to the thumbstick input
		bool aiming;
		float deadzone = 0.25f;
		Vector2 stickInput = new Vector2(inputDevice.RightStickX, inputDevice.RightStickY);
		if (stickInput.magnitude < deadzone) {
			aiming = false;
		} else {
			stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));
			aiming = true;
		}

		if (aiming) {
						float newRotation = Mathf.Atan2 (stickInput.y, stickInput.x) * Mathf.Rad2Deg; //Calculate the new angle to rotate to
						this.transform.Rotate (0f, 0f, newRotation - this.transform.eulerAngles.z + 90f); //Rotate
				}
		
		if (aiming && shotCool <= 0f) { //If the shot has cooled down and the player is aiming
			SpellcastStats myStats = GlobalControl.globalControl.stats; //Handle to the player stats
			shotCool += 100f - (myStats.stats[0] * 10f); //Add a value to the cooldown variable equal to the frequency stat times a flat multiplier
			GameObject mySpell = (GameObject)Instantiate (spell, origin.position, origin.rotation); //Instantiate a spell
			Vector2 direction = origin.position - this.transform.position; //Calculate a vector2 of direction based on player position and wand position
			mySpell.rigidbody2D.AddForce (direction * 600f); //Add the force in the direction calculated times the shotspeed stat
			mySpell.GetComponent<SpellShotScript> ().stats = myStats.ReturnCopy ();
			mySpell.GetComponent<SpellShotScript> ().myCombo = myCombos [selectedSpell];
			mySpell.GetComponent<SpellShotScript> ().myWand = this.gameObject;
			mySpell.GetComponent<SpellShotScript> ().rotationAtFire = this.transform.rotation;
		}
		
		
		if (shotCool > 0f) {
			shotCool -= 1f; //if the shot is still on cooldown, decriment the shot cooldown
		}
	}
}
