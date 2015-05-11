using UnityEngine;
using System.Collections;
using InControl;

public class CoffinScript : MonoBehaviour {

    public int deadMoney;

    public bool looted;

	// Use this for initialization
	void Start () {

        deadMoney = GlobalControl.globalControl.coffinCoins;

        if (deadMoney < 0)
            deadMoney = 2;

        looted = false;
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

    void OnCollisionStay2D(Collision2D col)
    {
        InputDevice inputDevice = InputManager.ActiveDevice;
        if (col.gameObject.tag == "Player" && inputDevice.Action1.WasPressed && looted == false)
        {
            GlobalControl.globalControl.coins = deadMoney;

        

            looted = true;

            //OUTPUT SHOULD INFORM PLAYER THAT THEY FOUND MONEY IN THE COFFIN!
            Debug.Log("The coffin holds a strangely familiar corpse. You find " + deadMoney + " coins as well as some magical runes...");
        }
    }
}
