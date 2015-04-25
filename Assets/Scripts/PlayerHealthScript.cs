using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour {

	public float HP = 100; //Health of the player
	public int minimumDeathPrice = 20; //Minimum price player can have for resurrecting
	
	//Function to inflict damage
	//Used for any logic like armor or conditional health decrimenting
	public void Decriment(float damage)
	{
		HP -= damage; //Subtract damage from health
		if (HP <= 0) { //If the health is at or below zero
			Die ();
		}
	}
	
	void Die()
	{
		int deduction = GlobalControl.globalControl.coins / 10;
		if (deduction < minimumDeathPrice)
						deduction = minimumDeathPrice;
		GlobalControl.globalControl.coins -= deduction;
		Application.LoadLevel ("OutsideWhiteBox");
	}
}
