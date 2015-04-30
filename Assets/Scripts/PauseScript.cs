using UnityEngine;
using System.Collections;
using InControl;

public class PauseScript : MonoBehaviour {

	public SpellcastScript myCaster;
	public static bool[] unlockedShapes = new bool[3];
	public static bool[] unlockedElements = new bool[6];

	private bool paused;
	private GUIButton[][] menus = new GUIButton[5][];
	private int menuState;
	private int index;
	private string headerText;
	private Rect headerRect;
	private int oldComboIndex;
	private int newComboShape;
	private int newComboElement;
	private SpellCombo newCombo;
	private bool minigame;

	public void Pause()
	{
		paused = true;
		Time.timeScale = 0.0f;
		minigame = true;
	}
	public void UnPause()
	{
		paused = false;
		Time.timeScale = 1.0f;
		minigame = false;
	}

	void Start()
	{
		paused = false;
		minigame = false;

		myCaster = GetComponentInChildren<SpellcastScript>();

		menus[0] = new GUIButton[3];
		menus [0] [0] = new GUIButton ("mainResume", "Resume");
		menus [0] [1] = new GUIButton ("mainSpellbook", "Spellbook");
		menus [0] [2] = new GUIButton ("mainQuit", "Quit");
		menus[1] = new GUIButton[5];
		menus [1] [0] = new GUIButton ("combo0", string.Empty);
		menus [1] [1] = new GUIButton ("combo1", string.Empty);
		menus [1] [2] = new GUIButton ("combo2", string.Empty);
		menus [1] [3] = new GUIButton ("combo3", string.Empty);
		menus [1] [4] = new GUIButton ("comboBack", "Back");
		menus[2] = new GUIButton[4];
		menus [2] [0] = new GUIButton ("shape0", "Circle");
		menus [2] [1] = new GUIButton ("shape1", "Line");
		menus [2] [2] = new GUIButton ("shape2", "Cluster");
		menus [2] [3] = new GUIButton ("shapeBack", "Back");
		menus[3] = new GUIButton[7];
		menus [3] [0] = new GUIButton ("element0", "Fire");
		menus [3] [1] = new GUIButton ("element1", "Spark");
		menus [3] [2] = new GUIButton ("element2", "Ice");
		menus [3] [3] = new GUIButton ("element3", "Poison");
		menus [3] [4] = new GUIButton ("element4", "Wind");
		menus [3] [5] = new GUIButton ("element5", "Earth");
		menus [3] [6] = new GUIButton ("elementBack", "Back");
		menus[4] = new GUIButton[2];
		menus [4] [0] = new GUIButton ("confirmYes", "Confirm");
		menus [4] [1] = new GUIButton ("confirmBack", "Back");

		menuState = 0;
		index = 0;

		headerText = "Paused";
		headerRect = new Rect (500f, 100f, 200f, 40f);

		oldComboIndex = 0;
		newComboShape = 0;
		newComboElement = 0;
		newCombo = new SpellCombo ();

		for (int ii = 0; ii < menus.Length; ii++) {
						for (int jj = 0; jj < menus[ii].Length; jj++) {
								menus [ii] [jj].SetRect (new Rect (500f, 140f + ((float)jj * 20f), 100f, 20f));
						}
				}
	}

	void OnGUI()
	{
		if (paused && !minigame) {
						GUI.Label (headerRect, headerText);

						for (int ii = 0; ii < menus[menuState].Length; ii++) {
								GUIButton current = menus [menuState] [ii];
								GUI.SetNextControlName (current.controlName);

								if (menuState == 2 && ii < menus [menuState].Length - 1 && unlockedShapes [ii] == false)
										GUI.enabled = false;
								if (menuState == 3 && ii < menus [menuState].Length - 1 && unlockedElements [ii] == false)
										GUI.enabled = false;

								GUI.Button (current.rect, current.text);

								GUI.enabled = true;
						}

						GUI.FocusControl (menus [menuState] [index].controlName);
				}
	}

	void Update()
	{
		InputDevice inputDevice = InputManager.ActiveDevice;
		if (inputDevice.MenuWasPressed) 
						PauseToggle ();
				else if (inputDevice.DPadUp.WasPressed) 
						menuSelection ("up");
				else if (inputDevice.DPadDown.WasPressed) 
						menuSelection ("down");
//				else if (inputDevice.Action1.WasPressed) 
//						ActivateButton ();
				
	}

	void PauseToggle()
	{
		if (paused) {
			Time.timeScale = 1.0f;
			ChangeMenu(0);
		} else {
			Time.timeScale = 0.0f;
		}
		paused = !paused;
	}

	void menuSelection(string direction)
	{
		int max = menus [menuState].Length - 1;
		if (direction == "up") {
			if (index == 0) {
				index = max;
			} else {
				index -= 1;
			}
		}
		
		if (direction == "down") {
			if (index == max) {
				index = 0;
			} else {
				index += 1;
			}
		}
	}

	void ActivateButton()
	{
		if (menuState == 0) {
						if (index == 0)
								PauseToggle ();
						else if (index == 1)
								ChangeMenu (1);
						else if (index == 2)
								print ("QUIT GAME");
				} else if (menuState == 4) {
						if (index == 0) {
								myCaster.myCombos [oldComboIndex].InitializeValues (newCombo.shape, newCombo.element);
								ChangeMenu (0);
						} else if (index == 1) {
								ChangeMenu (menuState - 1);
						}
				} else {
						if (index < menus [menuState].Length - 1) {
								switch (menuState) {
								case 1:
										oldComboIndex = index;
										ChangeMenu (menuState + 1);
										break;
								case 2:
										if (unlockedShapes [index] == true) {
												newComboShape = index;
												ChangeMenu (menuState + 1);
										}
										break;
								case 3:
										if (unlockedElements [index] == true) {
												newComboElement = index;
												newCombo.InitializeValues (newComboShape, newComboElement);
												ChangeMenu (menuState + 1);
										}
										break;
								}
						} else {
								ChangeMenu (menuState - 1);
						}
				}
			
	}

	void ChangeMenu(int whichMenu)
	{
		menuState = whichMenu;
		index = 0;

		switch (menuState) {
				case 0:
						headerText = "Paused";
						break;
				case 1:
						headerText = "Select combo to replace";
						for (int ii = 0; ii < menus[menuState].Length - 1; ii++) {
								SpellCombo comboHandle = myCaster.myCombos [ii];
								menus [menuState] [ii].text = comboHandle.shapeString + " " + comboHandle.elementString;
						}
						break;
				case 2:
						headerText = "Pick a Shape Rune";
						break;
				case 3:
						headerText = "Pick an Element Rune";
						break;
				case 4:
						SpellCombo oldCombo = myCaster.myCombos [oldComboIndex];
						headerText = "Replace " + oldCombo.shapeString + " " + oldCombo.elementString 
								+ " with " + newCombo.shapeString + " " + newCombo.elementString;
						break;
				}
	}
}
