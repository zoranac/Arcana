using UnityEngine;
using System.Collections;
using InControl;

public class CooldownManager : MonoBehaviour {

	float cooldown1;
	float cooldown2;
	float cooldown3;
	float cooldown4;

	public float cooldown;

	float cooldownTime1;
	float cooldownTime2;
	float cooldownTime3;
	float cooldownTime4;

	public GUITexture leftTrigger;
	public GUITexture leftBumper;
	public GUITexture rightBumper;
	public GUITexture rightTrigger;

	public GUIText timer1;
	public GUIText timer2;
	public GUIText timer3;
	public GUIText timer4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Cooldowns ();
		TimerUpdate ();
		Inputs ();
	}

	//function enabling cooldowns
	void Cooldowns () {
		//check if cooldown1 should be active
		if (cooldown1 > 0) {
			cooldown1 = cooldown1 - 1 * Time.deltaTime;

			//sets back to zero
			if(cooldown1 < 0) {
				cooldown1 = 0;
				leftTrigger.GetComponent<FadeManager>().activeState = false;
			}
		}

		//check if cooldown2 should be active
		if (cooldown2 > 0) {
			cooldown2 = cooldown2 - 1 * Time.deltaTime;
			
			//sets back to zero
			if(cooldown2 < 0) {
				cooldown2 = 0;
				leftBumper.GetComponent<FadeManager>().activeState = false;
			}
		}

		//check if cooldown3 should be active
		if (cooldown3 > 0) {
			cooldown3 = cooldown3 - 1 * Time.deltaTime;
			
			//sets back to zero
			if(cooldown3 < 0) {
				cooldown3 = 0;
				rightBumper.GetComponent<FadeManager>().activeState = false;
			}
		}

		//check if cooldown4 should be active
		if (cooldown4 > 0) {
			cooldown4 = cooldown4 - 1 * Time.deltaTime;
			
			//sets back to zero
			if(cooldown4 < 0) {
				cooldown4 = 0;
				rightTrigger.GetComponent<FadeManager>().activeState = false;
			}
		}
	}

	void Inputs() {
		//conditions for cooldown1 to trigger
		if (InputManager.ActiveDevice.LeftTrigger.WasPressed && cooldown1 <= 0) {
			//do ability 1 action
			QuickFixCooldown();
		}

		//conditions for cooldown2 to trigger
		if (InputManager.ActiveDevice.LeftBumper.WasPressed && cooldown2 <= 0) {
			//do ability 2 action
			QuickFixCooldown();
		}

		//conditions for cooldown3 to trigger
		if (InputManager.ActiveDevice.RightBumper.WasPressed && cooldown3 <= 0) {
			//do ability 3 action
			QuickFixCooldown();
		}

		//conditions for cooldown4 to trigger
		if (InputManager.ActiveDevice.RightTrigger.WasPressed && cooldown4 <= 0) {
			//do ability 4 action
			QuickFixCooldown();
		}
	}
	void QuickFixCooldown() {
		//setting cooldown length
		cooldown1 = cooldown;
		cooldown2 = cooldown;
		cooldown3 = cooldown;
		cooldown4 = cooldown;
		//do action/ability
		rightTrigger.GetComponent<FadeManager>().activeState = true;
		rightBumper.GetComponent<FadeManager>().activeState = true;
		leftBumper.GetComponent<FadeManager>().activeState = true;
		leftTrigger.GetComponent<FadeManager>().activeState = true;
	}

	void TimerUpdate() {
		if (cooldown1 != 0) {
			timer1.fontSize = 36;
			timer1.text = cooldown1.ToString().Substring(0,3);
		} else {
			timer1.fontSize = 28;
			timer1.text = "L T";
		}

		if (cooldown2 != 0) {
			timer2.fontSize = 36;
			timer2.text = cooldown2.ToString().Substring(0,3);
		} else {
			timer2.fontSize = 28;
			timer2.text = "L B";
		}

		if (cooldown3 != 0) {
			timer3.fontSize = 36;
			timer3.text = cooldown3.ToString().Substring(0,3);
		} else {
			timer3.fontSize = 28;
			timer3.text = "RB";
		}

		if (cooldown4 != 0) {
			timer4.fontSize = 36;
			timer4.text = cooldown4.ToString().Substring(0,3);
		} else {
			timer4.fontSize = 28;
			timer4.text = "RT";
		}
	}
	
}
