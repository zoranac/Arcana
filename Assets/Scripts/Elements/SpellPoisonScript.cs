using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellPoisonScript : SpellParentScript {

	public List<GameObject> enemiesDamaged = new List<GameObject>();

	private float modifier = 1f; //Variable used to half damage after the spell reaches its duration
	
	void Start () {
		Invoke ("Expire", stats.duration);
		Invoke ("Die", stats.duration * 2f); //Invoke the die function after however many seconds the duration stat is
		this.GetComponent<SpriteRenderer> ().color = Color.green; //Change the color of the spell to blue. DEFAULT
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
			if (enemiesDamaged.Contains(other.gameObject) == false) //Check if this enemy has been damaged by this spell before
			{
				enemiesDamaged.Add(other.gameObject); //If not, add it to the list of damaged enemies so none get hit multiple times
				other.gameObject.GetComponent<HealthScript>().Decriment(stats.damage / modifier); //Deal the flat damage stat 
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object exits the trigger area, check if it is an enemy
		}
	}

	void Expire()
	{
		modifier = 2f;
		this.GetComponent<SpriteRenderer> ().color = new Color (0f, .2f, 0f);
		enemiesDamaged.Clear ();
	}
}
