using UnityEngine;
using System.Collections;
using InControl;

public class PauseScript : MonoBehaviour {

	public SpellcastScript myCaster;

	bool[] collectedShapes; //The shapes the player has access to
	bool[] collectedElements; //The elements the player has access to
	//The list of buttons for each menu
	GUIButton[][] bMenus = new GUIButton[5][]
	{
		new GUIButton[3], //Selecting to resume quit or replace spells
		new GUIButton[5], //Selecting what combo to replace
		new GUIButton[4], //Selecting what shape
		new GUIButton[7], //Selecting what element
		new GUIButton[2]  //Confirming the selection
	};

	bool paused;
	int menuPoint; //Where is the player in the menu, used to select which buttons to display
	int current; //What button the player has selected
	int newComboIndex; //What combo to replace with the new one
	int newShape; //What shape to build a new combo out of
	int newElement; //What element to build a new combo out of
	SpellCombo newCombo; //New combo



	// Use this for initialization
	void Awake () {
		paused = false;
		menuPoint = 0;
		current = 0;
		newCombo = new SpellCombo();

		InitializeButtons ();
	}
	
	// Update is called once per frame
	void Update () {
		InputDevice inputDevice = InputManager.ActiveDevice;

		if (inputDevice.MenuWasPressed) { //Pause the game on start button
				paused = !paused;
				Time.timeScale = paused ? 0.0f : 1.0f;
		}

		if (paused) {
			//Use dpad to navigate the menu
			if (inputDevice.DPadDown.WasPressed) {
					current++;
			} else if (inputDevice.DPadUp.WasPressed) {
					current--;
			}
			current = Mathf.Clamp (current, 0, bMenus [menuPoint].Length - 1); //Clamp the current variable to stay an index for the menu buttons

			if (inputDevice.Action1.WasPressed) {
				//Activate what button was pressed
				switch (menuPoint) { //find what menu the player is in, and in each menu find out what button is selected
				case 0:
					switch (current) {
					case 0: //resume
						paused = !paused; //Unpause the game
						Time.timeScale = paused ? 0.0f : 1.0f;
						break;
					case 1: //spellbook
						menuPoint = 1; //Navigate to spellbook menu
						current = 0;
						break;
					case 2: //quit
						Application.Quit (); //quit the game
						break;
					}
					break;
				case 1:
					if (current == bMenus [menuPoint].Length - 1) { //Back
							menuPoint = 0; //return to main
					} else { //Selecting a combo
							newComboIndex = current; //Get the index of the combo to replace
							menuPoint = 2;
					}
					current = 0; //Reset selected button
					break;
				case 2:
					if (current == bMenus [menuPoint].Length - 1) { //Back
							menuPoint = 1; //return to combo selection
					} else { //Selecting a shape
							newShape = current; //Get the index of the new shape
							menuPoint = 3;
					}
					current = 0; //Reset selected button
					break;
				case 3:
					if (current == bMenus [menuPoint].Length - 1) { //Back
							menuPoint = 2; //return to shape selection
					} else { //Selecting an element
							newElement = current; //Get the index of the new element
							menuPoint = 4;
					}
					current = 0; //Reset selected button
					break;
				case 4:
					if (current == bMenus [menuPoint].Length - 1) { //Back
						menuPoint = 3; //return to element selection
					} else { //confirming replacement
						myCaster.myCombos[newComboIndex] = newCombo;
						menuPoint = 0;
					}
					current = 0; //Reset selected button
					break;
				}
			}
		}
	}
	

	void OnGUI()
	{
		if (paused) { //Check if paused
			for (int ii = 0; ii < bMenus[menuPoint].Length; ii++) { //Cycle through whatever menu the player is on
				
				//Fills in the names for the combos if on the combo menu
				if (menuPoint == 1 && ii < bMenus[menuPoint].Length - 1) {
					bMenus[menuPoint][ii].text = myCaster.myCombos[ii].shape + " " + myCaster.myCombos[ii].element;
				//Sets buttons for collected shapes active and non collected shapes inactive
				} else if (menuPoint == 2 && ii < bMenus[menuPoint].Length - 1) {
					GUI.enabled = collectedShapes [ii];
				} else if (menuPoint == 3 && ii < bMenus[menuPoint].Length - 1) {
					GUI.enabled = collectedElements [ii];
				//Sets the text field to the combo the player has just built
				} else if (menuPoint == 4 && ii < bMenus[menuPoint].Length - 1) {
					newCombo.InitializeValues(newShape, newElement);
					bMenus[menuPoint][ii].text = newCombo.shape + " " + newCombo.element;
				}
				
				GUI.SetNextControlName (bMenus [menuPoint] [ii].controlName);
				GUI.Button (bMenus [menuPoint] [ii].rect, bMenus [menuPoint] [ii].text);
				
				GUI.enabled = true; //Re enable the gui no matter what after drawing a button
			}
			GUI.FocusControl(bMenus[menuPoint][current].controlName);
		}
	}

	#region Initialize Buttons
	void InitializeButtons()
	{
		//Fields marked as empty must be set in ongui
		bMenus [0] [0].SetValues ("unpause", "Resume");
		bMenus [0] [1].SetValues ("changespell", "Spellbook");
		bMenus [0] [2].SetValues ("endgame", "Quit");

		bMenus [1] [0].SetValues ("combo1", string.Empty);
		bMenus [1] [1].SetValues ("combo2", string.Empty);
		bMenus [1] [2].SetValues ("combo3", string.Empty);
		bMenus [1] [3].SetValues ("combo4", string.Empty);
		bMenus [1] [4].SetValues ("comboback", "Back");
		
		bMenus [2] [0].SetValues ("shape1", "Circle");
		bMenus [2] [1].SetValues ("shape2", "Line");
		bMenus [2] [2].SetValues ("shape3", "Cluster");
		bMenus [2] [3].SetValues ("shapeback", "Back");
		
		bMenus [3] [0].SetValues ("element1", "Fire");
		bMenus [3] [1].SetValues ("element2", "Spark");
		bMenus [3] [2].SetValues ("element3", "Ice");
		bMenus [3] [3].SetValues ("element4", "Poison");
		bMenus [3] [4].SetValues ("element5", "Wind");
		bMenus [3] [5].SetValues ("element6", "Earth");
		bMenus [3] [6].SetValues ("elementback", "Back");

		bMenus [4] [0].SetValues ("selectionyes", string.Empty);
		bMenus [4] [1].SetValues ("selectionback", "Back");

		for (int ii = 0; ii < bMenus.Length; ii++) {
						for (int jj = 0; jj < bMenus[ii].Length; jj++) {
								bMenus [ii] [jj].SetRect (new Rect (10f, (20f * (float)jj) + 10f, 100f, 20f));
						}
				}
	}
	#endregion
}
