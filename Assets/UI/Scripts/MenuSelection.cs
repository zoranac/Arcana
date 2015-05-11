using UnityEngine;
using System.Collections;
using InControl;

public class MenuSelection : MonoBehaviour {

	public int menuIndex;
	public AudioClip clip;
	public AudioClip clip2;
	public AudioClip clip3;

    GameObject names;

	bool inSubMenu = false;
	//public AnimationClip inClip;
	//public AnimationClip outClip;
	//public Animation anim;
	//public GameObject animationMaster;
	// Use this for initialization
	void Start () {
		//anim = animationMaster.GetComponent<Animator> ();
        names = GameObject.Find("Names");
	}
	
	// Update is called once per frame
	void Update () {
		//debug input
        if (InputManager.ActiveDevice.LeftStick.Up.WasPressed || InputManager.ActiveDevice.DPadUp.WasPressed && !inSubMenu)
        {
            audio.PlayOneShot(clip);
			menuIndex--;
			if(menuIndex < 0) {
				menuIndex = 0;
			}
		}

		//debug input
        if (InputManager.ActiveDevice.LeftStick.Down.WasPressed || InputManager.ActiveDevice.DPadDown.WasPressed && !inSubMenu)
        {
            audio.PlayOneShot(clip);
			menuIndex++;
			if(menuIndex > 2) {
				menuIndex = 2;
			}
		}

		//checks which menu item is selected
		IndexUpdate ();
	
		//back out of submenu (options/credits)
		if (inSubMenu && Input.GetKeyDown(KeyCode.O)) {
			inSubMenu = false;
			audio.PlayOneShot(clip3);
		}
	}

	void IndexUpdate () {
		switch (menuIndex) {
		case 0:
			//highlight new game
			GameObject.Find("New Game").GetComponent<Highlight>()._Highlight();
			GameObject.Find("Options").GetComponent<Highlight>().Unhighlight();
			GameObject.Find("Credits").GetComponent<Highlight>().Unhighlight();
           // names.SetActive(false);
            if (InputManager.ActiveDevice.Action1.WasPressed )
            {
				//play sound effect
				audio.PlayOneShot(clip2);

				//start new game/load scene
				Application.LoadLevel(1);
				print ("Start new game");
			}
			break;

		case 1:
			//highlight options
			GameObject.Find("New Game").GetComponent<Highlight>().Unhighlight();
			GameObject.Find("Options").GetComponent<Highlight>()._Highlight();
			GameObject.Find("Credits").GetComponent<Highlight>().Unhighlight();
           // names.SetActive(false);
            if (InputManager.ActiveDevice.Action1.WasPressed)
            {
				//play sound effect
				audio.PlayOneShot(clip2);
				//show options
				print ("Show Options");
               
				//inSubMenu = true;
			}
			break;

		case 2:
			//highlight credits
			GameObject.Find("New Game").GetComponent<Highlight>().Unhighlight();
			GameObject.Find("Options").GetComponent<Highlight>().Unhighlight();
			GameObject.Find("Credits").GetComponent<Highlight>()._Highlight();
            if (InputManager.ActiveDevice.Action1.WasPressed)
            {
				//play sound effect
				audio.PlayOneShot(clip2);
				//show credits
                //names.SetActive(true);
				//inSubMenu = true;
			}
			break;
		
		default:
			break;
			}
	}

}
