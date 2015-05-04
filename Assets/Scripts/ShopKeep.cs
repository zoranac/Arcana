using UnityEngine;
using System.Collections;
using InControl;

public class ShopKeep : MonoBehaviour {

	// Use this for initialization
	void Start () {

        
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision2D col)
    {
        InputDevice inputDevice = InputManager.ActiveDevice;
        if (col.gameObject.tag == "Player" && inputDevice.Action1.WasPressed)
        {
            this.gameObject.GetComponent<ShopScript>().shopping = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<ShopScript>().shopping = false;
        }
    }
}
