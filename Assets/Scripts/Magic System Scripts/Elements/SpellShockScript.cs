using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellShockScript : SpellParentScript {
	
	void Start () {
		Invoke ("Die", stats.stats[2]); //Invoke the die function after however many seconds the duration stat is
		this.GetComponent<SpriteRenderer> ().color = Color.yellow; //Change the color of the spell to yellow. DEFAULT
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
            other.gameObject.GetComponent<HealthScript>().Decriment(stats.stats[3]);
		}
	}
}
