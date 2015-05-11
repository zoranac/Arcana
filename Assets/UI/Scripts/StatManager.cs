using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour {

	public float health;
	public float special = 0;
	public int coins = 0;

	float maxHealth;
	public float maxSpecial;

	public GUIText coinCount;
	public GUITexture healthBar;
	public GUITexture specialBar;

	public AudioClip coinPickup;
	public AudioClip specialReady;

	// Use this for initialization
	void Start () {
		//set values for maxes
		maxHealth = healthBar.pixelInset.width;
		maxSpecial = specialBar.pixelInset.width;

		//set health to maxhealth value
        health = maxHealth;
        
	}
	
	// Update is called once per frame
	void Update () {
		//update guitext with coin count
		coinCount.text = ("x" + GlobalControl.globalControl.coins.ToString ());
		//update healthbar with health count
		healthBar.pixelInset = new Rect (-105, -10, health, 20);
		//update specialbar with special count
		specialBar.pixelInset = new Rect (-105, -5, special, 10);

		//health debug value modification
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Health (-17f);
			GameObject.Find("Health").GetComponent<HealthBarFlash>().FlashBar();
		}
		if (Input.GetKeyDown (KeyCode.RightArrow))
			Health (13f);

		//special debug value modification
		if (Input.GetKeyDown (KeyCode.DownArrow))
			Special (-maxSpecial);
		
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Special (23f);

		//debug add coin
		if (Input.GetKeyDown (KeyCode.A))
			AddCoin (1);
	}

	//add coin to count
	public void AddCoin (int coinAmount) {
		audio.PlayOneShot(coinPickup);
		coins = coins + coinAmount;
	}

	//health damage/receive calculations
	public void Health (float passedFloat) {
		health = health + passedFloat;
		if (health > maxHealth) {
			health = maxHealth;
		}

		if (health < 0) {
			health = 0;
		}
	}

	//special value calucation
	public void Special (float passedFloat) {
		special = special + passedFloat;
		if (special > maxSpecial) {
			special = maxSpecial;
			//notification that special is ready
			audio.PlayOneShot(specialReady);
			GameObject.Find("Special").GetComponent<Pulse>().specialReady = true;
		}

		if (special < maxSpecial) {
			//special is not ready, do not pulse
			GameObject.Find("Special").GetComponent<Pulse>().specialReady = false;
		}

		if (special < 0) {
			special = 0;
		}
	}

    public void SpecialReset()
    {
        special = 0;
    }
}
