using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellFrostScript : SpellParentScript {

	public List<GameObject> enemiesDamaged = new List<GameObject>();

	void Start () {
		Invoke ("Die", stats.duration); //Invoke the die function after however many seconds the duration stat is
		this.GetComponent<SpriteRenderer> ().color = Color.blue; //Change the color of the spell to blue. DEFAULT
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
			if (enemiesDamaged.Contains(other.gameObject) == false) //Check if this enemy has been damaged by this spell before
			{
				enemiesDamaged.Add(other.gameObject); //If not, add it to the list of damaged enemies so none get hit multiple times
				other.gameObject.GetComponent<HealthScript>().Decriment(stats.damage / 2f); //Deal the flat damage stat 
			}
			//INPUT CODE HERE TO ADD A SLOW EFFECT TO THE ENEMY
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object exits the trigger area, check if it is an enemy
			//INPUT CODE HERE TO REMOVE SLOW EFFECT FROM THE ENEMY
		}
	}
}
