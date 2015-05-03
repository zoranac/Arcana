using UnityEngine;
using System.Collections;
using InControl;

public class ShopScript : MonoBehaviour {

	public float menuTop = 500f;
	public float menuLeft = 100f;
	public float menuButtonWidth = 100f;
	public float menuButtonHeight = 20f;
	public int runePrice = 50;
	public int basePointPrice = 20;
	public bool shopping;

	private bool readyToBuy;
	private bool purchaseJustMade;
	private bool purchaseJustCanceled;
	private GUIButton[][] menus = new GUIButton[3][];
	private int menuState;
	private int index;
	private int[] purchaseIndex;
	private Rect headerRect;
	private string headerText;
	private string purchaseString;

	void Start()
	{
		shopping = false;
		readyToBuy = false;
		purchaseJustMade = false;
		purchaseJustCanceled = false;

		menus [0] = new GUIButton[3];
		menus [0] [0] = new GUIButton ("mainRunes", "Runes");
		menus [0] [1] = new GUIButton ("mainTraining", "Training");
		menus [0] [2] = new GUIButton ("mainBack", "Leave");
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

		Rect newRect;
		for (int ii = 0; ii < menus.Length; ii++) {
						for (int jj = 0; jj < menus[ii].Length; jj++) {
								newRect = new Rect (menuLeft, menuTop + ((float)jj * menuButtonHeight), menuButtonWidth, menuButtonHeight);
								menus [ii] [jj].SetRect (newRect);
						}
				}

		menuState = 0;
		index = 0;
		purchaseIndex = new int[]{0, 0};
		
		headerRect = new Rect(0, Screen.height - 100f, Screen.width, 100f);
		headerText = "I AM ERROR";
		purchaseString = string.Empty;
	}
	
	void OnGUI()
	{
		if (shopping) {
				for (int ii = 0; ii < menus[menuState].Length; ii++) {
						GUI.Box(headerRect, headerText);

						GUIButton current = menus [menuState] [ii];
						GUI.SetNextControlName (current.controlName);

						string displayText = string.Empty;
						switch (menuState) {
						case 1:
								if (ii < 3) {
										GUI.enabled = CheckIfRuneViablePurchase (0, ii);
										displayText = runePrice + "g ";
								} else if (ii < 9) {
										GUI.enabled = CheckIfRuneViablePurchase (1, ii - 3);
										displayText = runePrice + "g ";
								} else if (ii < 11) {
										GUI.enabled = readyToBuy;
								}
								break;
						case 2:
								if (ii < 4) {
										GUI.enabled = CheckIfStatViablePurchase(ii);
										displayText = (basePointPrice * (int)GlobalControl.globalControl.stats.stats[ii]) + "g ";
								} else if (ii < 6) {
										GUI.enabled = readyToBuy;
								}
								break;
						}
						displayText += current.text;
						GUI.Button (current.rect, displayText);

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
						else if (inputDevice.Action1.WasPressed) 
								ActivateButton ();
				}
	}

	void ShopToggle()
	{
		ChangeMenu (0);
		ClearFlags ();
		UpdateHeader ();
		shopping = !shopping;
	}

	bool CheckIfStatViablePurchase(int index)
	{
		if (readyToBuy == true || GlobalControl.globalControl.stats.atCap [index] == true || GlobalControl.globalControl.coins < GlobalControl.globalControl.stats.stats[index] * basePointPrice)
						return false;

		return true;
	}

	bool CheckIfRuneViablePurchase(int type, int index)
	{
		if (readyToBuy == true || GlobalControl.globalControl.coins < runePrice) 
						return false;
				else if (type == 0 && PauseScript.unlockedShapes [index] == true) 
						return false;
				else if (type == 1 && PauseScript.unlockedElements [index] == true)
						return false;
				else
						return true;
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

	void ChangeMenu(int whichMenu)
	{
		menuState = whichMenu;
		if (menuState != 0)
						index = menus [menuState].Length - 1;
				else
						index = 0;
	}

	void ClearFlags()
	{
		readyToBuy = false;
		purchaseJustMade = false;
		purchaseJustCanceled = false;
	}
	
	void ActivateButton()
	{
		purchaseJustMade = false;
		purchaseJustCanceled = false;
		switch (menuState) {
				case 0:
						switch (index) {
						case 0:
								ChangeMenu (1);
								break;
						case 1:
								ChangeMenu (2);
								break;
						case 2:
								ShopToggle ();
								break;
						}
						break;
				case 1:
						if (index < 3) {
								if (CheckIfRuneViablePurchase (0, index)) {
										purchaseIndex [0] = 0;
										purchaseIndex [1] = index;
										UpdatePurchaseString ();
										readyToBuy = true;
										index = 9;
								}
						} else if (index < 9) {
								if (CheckIfRuneViablePurchase (1, index - 3)) {
										purchaseIndex [0] = 1;
										purchaseIndex [1] = index - 3;
										UpdatePurchaseString ();
										readyToBuy = true;
										index = 9;
								}
						} else if (index < 11) {
								if (readyToBuy) {
										if (index == 9) {
												ApplyPurchase ();
												purchaseJustMade = true;
												readyToBuy = false;
												index = 11;
										} else if (index == 10) {
												purchaseJustCanceled = true;
												readyToBuy = false;
												index = 11;
										}
								}
						} else if (index == 11) {
								ChangeMenu (0);
						}
						break;
				case 2:
						if (index < 4) {
								if (CheckIfStatViablePurchase (index)) {
										purchaseIndex [0] = 2;
										purchaseIndex [1] = index;
										UpdatePurchaseString ();
										readyToBuy = true;
										index = 4;
								}
						} else if (index < 6) {
								if (readyToBuy) {
										if (index == 4) {
												ApplyPurchase ();
												purchaseJustMade = true;
												readyToBuy = false;
												index = 6;
										}
										if (index == 5) {
												purchaseJustCanceled = true;
												readyToBuy = false;
												index = 6;
										}
								}
						} else if (index == 6) {
								ChangeMenu (0);
						}
						break;
				}
		UpdateHeader ();
	}

	void ApplyPurchase()
	{
		switch (purchaseIndex [0]) {
				case 0:
						GlobalControl.globalControl.coins -= runePrice;
						PauseScript.unlockedShapes [purchaseIndex [1]] = true;
						break;
				case 1:
						GlobalControl.globalControl.coins -= runePrice;
						PauseScript.unlockedElements [purchaseIndex [1]] = true;
						break;
				case 2:
						GlobalControl.globalControl.coins -= basePointPrice * (int)GlobalControl.globalControl.stats.stats [purchaseIndex [1]];
						GlobalControl.globalControl.stats.IncrementValue (purchaseIndex [1]);
						break;
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
				} else if (purchaseIndex [0] == 2) {
						switch (purchaseIndex [1]) {
						case 0:
								purchaseString = "Fire Rate";
								break;
						case 1:
								purchaseString = "Spell Size";
								break;
						case 2:
								purchaseString = "Spell Duration";
								break;
						case 3:
								purchaseString = "Spell Damage";
								break;
						}
				}
	}

	void UpdateHeader()
	{
		switch (menuState) {
				case 0:
						headerText = "Welcome! I sell runes at my shop for a flat price, or I can train your skills as a spellcaster.";
						break;
				case 1:
						if (purchaseJustMade)
								headerText = "Thank you for your coinage. Good luck out there.";
						else if (purchaseJustCanceled)
								headerText = "Alright, hope you find something else you like.";
						else if (readyToBuy)
								headerText = "Would you like to buy the " + purchaseString + " Rune for " + runePrice + " gold?";
						else
								headerText = "I can get you new pages for your spellbook...for a price.";
						break;
				case 2:
						if (purchaseJustMade)
								headerText = "...And that's enough training for this lesson. Come back any time.";
						else if (purchaseJustCanceled)
								headerText = "If you're sure. Don't get overconfident now.";
						else if (readyToBuy) {
								int statValue = (int)GlobalControl.globalControl.stats.stats [purchaseIndex [1]];
								int statPrice = statValue * basePointPrice;
								headerText = "Do you want to take lesson " + statValue + " in " + purchaseString + " for " + statPrice + " gold?";
						} else
								headerText = "I'm an old spellcaster myself. I can teach you the critical skills to make your spells devestating. Beginner lessons are cheap, expert...less so.";
						break;
				}
	}
}
