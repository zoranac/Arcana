using UnityEngine;
using System.Collections;
using InControl;

public class CoffinScript : MonoBehaviour {

    public int deadMoney;

    public bool looted;

    public bool showingMessage;

	// Use this for initialization
	void Start () {

        deadMoney = GlobalControl.globalControl.coffinCoins;

        if (deadMoney < 0)
            deadMoney = 2;

        looted = false;
        showingMessage = false;
	
	}
	
	void OnGUI()
    {
        if (showingMessage)
        {
            if (deadMoney == 2)
            {
                GUI.Box(new Rect(0, Screen.height - 100f, Screen.width, 100f), "This man has been picked clean- only some magical runes remain, along with the coins on his eyes. You take them anyway.");
            }
            
            else
            {
                GUI.Box(new Rect(0, Screen.height - 100f, Screen.width, 100f), "The coffin holds a strangely familiar corpse. You find " + deadMoney + " coins as well as some magical runes...");
            }
        } 
    }

    void OnCollisionStay2D(Collision2D col)
    {
        InputDevice inputDevice = InputManager.ActiveDevice;
        if (col.gameObject.tag == "Player" && inputDevice.Action1.WasPressed && looted == false)
        {
            GlobalControl.globalControl.coins = deadMoney;
           
        

            looted = true;
            showingMessage = true;
            Invoke("CancelMessage", 5f);

        }
    }

    void CancelMessage()
    {
        showingMessage = false;
        deadMoney = 0;
    }
}
