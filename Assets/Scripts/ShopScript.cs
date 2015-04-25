using UnityEngine;
using System.Collections;
using InControl;

public class ShopScript : MonoBehaviour {

	public float menuTop = 500f;
	public float menuLeft = 100f;
	public float menuButtonWidth = 100f;
	public float menuButtonHeight = 20f;
	public int runePrice = 50;

	private bool shopping;
	private GUIButton[][] menus = new GUIButton[3][];
	private int menuState;
	private int index;
	private string headerText;
	private Rect headerRect;
	private bool readyToBuy;
	private int[] purchaseIndex;
	private bool purchaseJustMade;
	private bool purchaseJustCanceled;
	private string purchaseString;
	private int trainingIndex;
	private string trainingString;
	private float[] statsAsArray;

	void Start()
	{
		shopping = false;

		menus [0] = new GUIButton[3];
		menus [0] [0] = new GUIButton ("mainRunes", "Runes");
		menus [0] [1] = new GUIButton ("mainTraining", "Training");
		menus [0] [2] = new GUIButton ("mainBack", "No");
		menus [1] = new GUIButton[12];
		menus [1] [0] = new GUIButton ("shoppingShape0", "Circle");
		menus [1] [1] = new GUIButton ("shoppingShape1", "Line");
		menus [1] [2] = new GUIButton ("shoppingShape2", "Cluster");
		menus [1] [3] = new GUIButton ("shoppingElement0", "Fire");
		menus [1] [4] = new GUIButton ("shoppingElement1", "Spark");
		menus [1] [5] = new GUIButton ("shoppingElement2", "Ice");
		menus [1] [6] = new GUIButton ("shoppingElement3", "Poison");
		menus [1] [7] = new GUIButton ("shoppingElement4", "Wind");
		menus [1] [8] = new GUIButton ("shoppingElement5", "Earth");
		menus [1] [9] = new GUIButton ("shoppingConfirm", "Yes");
		menus [1] [10] = new GUIButton ("shoppingDeny", "No");
		menus [1] [11] = new GUIButton ("shoppingBack", "Back");
		menus [2] = new GUIButton[7];
		menus [2] [0] = new GUIButton ("leveling1", "Fire Rate");
		menus [2] [1] = new GUIButton ("leveling2", "Spell Size");
		menus [2] [2] = new GUIButton ("leveling3", "Spell Duration");
		menus [2] [3] = new GUIButton ("leveling4", "Spell Damage");
		menus [2] [4] = new GUIButton ("levelingConfirm", "Yes");
		menus [2] [5] = new GUIButton ("levelingDeny", "No");
		menus [2] [6] = new GUIButton ("levelingBack", "Back");

		menuState = 0;
		index = 0;
		
		headerText = "I am ERROR";
		headerRect = new Rect (0f, Screen.height - 200f, Screen.width, 200f);

		Rect newRect;
		for (int ii = 0; ii < menus[0].Length; ii++) {
						newRect = new Rect (menuLeft, menuTop + ((float)ii * menuButtonHeight), menuButtonWidth, menuButtonHeight);
						menus [0] [ii].SetRect (newRect);
				}
		for (int ii = 0; ii < menus[1].Length; ii++) {
						if (ii < 3)
								newRect = new Rect (menuLeft, menuTop + ((float)ii * menuButtonHeight), menuButtonWidth, menuButtonHeight);
						else if (ii < 9)
								newRect = new Rect (menuLeft + menuButtonWidth, menuTop + ((float)(ii - 3) * menuButtonHeight), menuButtonWidth, menuButtonHeight);
						else
								newRect = new Rect (menuLeft, menuTop + ((float)(ii - 6) * menuButtonHeight), menuButtonWidth, menuButtonHeight);
						menus [1] [ii].SetRect (newRect);
				}
		for (int ii = 0; ii < menus[2].Length; ii++) {
						newRect = new Rect (menuLeft, menuTop + ((float)ii * menuButtonHeight), menuButtonWidth, menuButtonHeight);
						menus [2] [ii].SetRect (newRect);
				}

		readyToBuy = false;
		purchaseIndex = new int[2];
		purchaseJustMade = false;
		purchaseJustCanceled = false;
		statsAsArray = new float[4];
	}
	
	void OnGUI()
	{
		if (shopping) {
						UpdateHeader ();
						GUI.Box (headerRect, headerText);

						for (int ii = 0; ii < menus[menuState].Length; ii++) {
								GUIButton current = menus [menuState] [ii];
								GUI.SetNextControlName (current.controlName);

								GUI.enabled = false;
								if (menuState == 0) {
										GUI.enabled = true;
								} else if (menuState == 1) {
										if (ii > 10)
												GUI.enabled = true;
										else if (ii > 8 && readyToBuy)
												GUI.enabled = true;
										else if (ii > 2 && CheckIfRuneViablePurchase (1, ii - 3))
												GUI.enabled = true;
										else if (ii >= 0 && CheckIfRuneViablePurchase (0, ii))
												GUI.enabled = true;
								} else if (menuState == 2) {
										if (ii > 3 && readyToBuy)
												GUI.enabled = true;
										else if (ii >= 0 && CheckIfStatViablePurchase (ii))
												GUI.enabled = true;
								}
				
								GUI.Button (current.rect, current.text);
				
								GUI.enabled = true;
						}
						GUI.FocusControl (menus [menuState] [index].controlName);
				}
	}

	void Update()
	{
		if (shopping) {
						InputDevice inputDevice = InputManager.ActiveDevice;
						if (inputDevice.DPadUp.WasPressed) 
								menuSelection ("up");
						else if (inputDevice.DPadDown.WasPressed) 
								menuSelection ("down");
						else if (inputDevice.DPadLeft.WasPressed)
								menuSelection ("left");
						else if (inputDevice.DPadRight.WasPressed)
								menuSelection ("right");
						else if (inputDevice.Action1.WasPressed) 
								ActivateButton ();
						else if (inputDevice.MenuWasPressed)
								ShopToggle ();
				}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
						ShopToggle ();
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
						ShopToggle ();
	}

	bool CheckIfStatViablePurchase(int index)
	{
		UpdateStatArray ();

		if (GlobalControl.globalControl.stats.atCap [index] == true || GlobalControl.globalControl.coins < (int)statsAsArray[index] * 20)
						return false;

		return true;
	}

	bool CheckIfRuneViablePurchase(int type, int index)
	{
		if (GlobalControl.globalControl.coins < runePrice) 
						return false;
				else if (type == 0 && PauseScript.unlockedShapes [index] == true) 
						return false;
				else if (type == 1 && PauseScript.unlockedElements [index] == true)
						return false;
				else
						return true;
	}
	
	void ShopToggle()
	{
		ChangeMenu (0);
		ClearFlags ();
		shopping = !shopping;
	}

	void ClearFlags()
	{
		readyToBuy = false;
		purchaseJustMade = false;
		purchaseJustCanceled = false;
	}
	
	void menuSelection(string direction)
	{
		if (menuState == 0 || menuState == 2) {
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
				} else if (menuState == 1) {
						if (direction == "up") {
								if (index == 0)
										index = 11;
								else if (index == 3)
										index = 6;
								else if (index == 9)
										index = 2;
								else
										index -= 1;
						} else if (direction == "down") {
								if (index == 2)
										index = 9;
								else if (index == 6)
										index = 3;
								else if (index == 11)
										index = 0;
						} else if (direction == "left" || direction == "right") { //Because there are only two collumns this works for both
								if (index < 3)
										index += 3;
								else if (index < 6)
										index -= 3;
								else if (index < 9)
										index += 3;
								else
										index -= 3;
						}
				}
	}
	
	void ActivateButton()
	{
		if (menuState == 0) {
						if (index == 0)
								ChangeMenu (1);
						else if (index == 1)
								ChangeMenu (2);
						else if (index == 2)
								ShopToggle ();
				} else if (menuState == 1) {
						purchaseJustMade = false;
						purchaseJustCanceled = false;
						if (readyToBuy) {
								if (index == 9) {
										readyToBuy = false;
										purchaseJustMade = true;
										switch (purchaseIndex [0]) {
										case 0:
												PauseScript.unlockedShapes [purchaseIndex [1]] = true;
												break;
										case 1:
												PauseScript.unlockedElements [purchaseIndex [1]] = true;
												break;
										}
								} else if (index == 10) {
										readyToBuy = false;
										purchaseJustCanceled = true;
								}
						} else {
								if (index < 3 && CheckIfRuneViablePurchase (0, index)) {
										readyToBuy = true;
										purchaseIndex [0] = 0;
										purchaseIndex [1] = index;
										UpdatePurchaseString ();
								} else if (index < 9 && CheckIfRuneViablePurchase (1, index - 3)) {
										readyToBuy = true;
										purchaseIndex [0] = 1;
										purchaseIndex [1] = index - 3;
										UpdatePurchaseString ();
								}
						}

						if (index == 11) {
								ChangeMenu (0);
								ClearFlags ();
						}
				} else if (menuState == 2) {
						purchaseJustMade = false;
						purchaseJustCanceled = false;
						if (readyToBuy) {
								if (index == 4) {
										readyToBuy = false;
										purchaseJustMade = true;
										GlobalControl.globalControl.stats.IncrementValue (trainingIndex);
										UpdateStatArray ();
								} else if (index == 5) {
										readyToBuy = false;
										purchaseJustCanceled = true;
								}
						} else {
								if (index < 4 && CheckIfStatViablePurchase (index)) {
										readyToBuy = true;
										trainingIndex = index;
										UpdateTrainingString ();
								}
						}
			
						if (index == 6) {
								ChangeMenu (0);
								ClearFlags ();
						}
				}
	}

	void UpdatePurchaseString()
	{
		if (purchaseIndex [0] == 0) {
						switch (purchaseIndex [1]) {
						case 0:
								purchaseString = "Circle";
								break;
						case 1:
								purchaseString = "Line";
								break;
						case 2:
								purchaseString = "Cluster";
								break;
						}
				} else if (purchaseIndex [0] == 1) {
						switch (purchaseIndex [1]) {
						case 0:
								purchaseString = "Fire";
								break;
						case 1:
								purchaseString = "Spark";
								break;
						case 2:
								purchaseString = "Ice";
								break;
						case 3:
								purchaseString = "Poison";
								break;
						case 4:
								purchaseString = "Wind";
								break;
						case 5:
								purchaseString = "Earth";
								break;
						}
				}
	}

	void UpdateTrainingString()
	{
		switch (trainingIndex) {
				case 0:
						trainingString = "Fire Rate";
						break;
				case 1:
						trainingString = "Spell Size";
						break;
				case 2:
						trainingString = "Spell Duration";
						break;
				case 3:
						trainingString = "Spell Damage";
						break;
				}
	}

	void UpdateHeader()
	{
		if (menuState == 0) {
								headerText = "Welcome, would you like to shop?";
				} else if (menuState == 1) {
						if (purchaseJustMade)
								headerText = "Thank you for your business.";
						else if (purchaseJustCanceled)
								headerText = "Alright, tell me if you find something else you like.";
						else if (readyToBuy) 
								headerText = "Would you like to buy the " + purchaseString + " rune for " + runePrice + "coins?";
						else
								headerText = "All runes cost " + runePrice + " coins. What would you like to buy?";
		} else if (menuState == 2) {
			if (purchaseJustMade)
				headerText = "Thank you for your business.";
			else if (purchaseJustCanceled)
				headerText = "Alright, tell me if you find something else you like.";
			else if (readyToBuy)
				headerText = "Would you like to train " + trainingString
					+ " to level " + ((int)statsAsArray[trainingIndex]+1)
					+ " for " + ((int)statsAsArray[trainingIndex] * 20) + " coins?";
			else
				headerText = "Your stats are "
					+ statsAsArray[0] + " for Frequency, "
					+ statsAsArray[1] + " for Spell Size, "
					+ statsAsArray[2] + " for Spell Duration, and"
					+ statsAsArray[3] + " for Spell Damage. " 
					+ "What would you like to buy?";
		}
	}

	void UpdateStatArray()
	{
		statsAsArray [0] = GlobalControl.globalControl.stats.frequency;
		statsAsArray [1] = GlobalControl.globalControl.stats.size;
		statsAsArray [2] = GlobalControl.globalControl.stats.duration;
		statsAsArray [3] = GlobalControl.globalControl.stats.damage;
	}
	
	void ChangeMenu(int whichMenu)
	{
		menuState = whichMenu;
		index = 0;
		UpdateHeader ();
	}
}
