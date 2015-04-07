using UnityEngine;
using System.Collections;
using InControl;

public class Movement : MonoBehaviour {

    public float moveSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        InputDevice inputDevice = InputManager.ActiveDevice;
        //If the J button is hit, move left
        if (Input.GetKey("j") || inputDevice.DPadLeft.IsPressed)
        {
            this.gameObject.transform.Translate(moveSpeed * Vector3.left * Time.deltaTime);
        }

            //If the L button is hit, move right
        if (Input.GetKey("l") || inputDevice.DPadRight.IsPressed)
            {
                this.gameObject.transform.Translate(moveSpeed * Vector3.right * Time.deltaTime);
            }

            //If the I button is hit, move up
        if (Input.GetKey("i") || inputDevice.DPadUp.IsPressed)
            {
                this.gameObject.transform.Translate(moveSpeed * Vector3.up * Time.deltaTime);
            }

            //If the K button is hit, move down
        if (Input.GetKey("k") || inputDevice.DPadDown.IsPressed)
            {
                this.gameObject.transform.Translate(moveSpeed * Vector3.down * Time.deltaTime);
            }
        
	}
}
