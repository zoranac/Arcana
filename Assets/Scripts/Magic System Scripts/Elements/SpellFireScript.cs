using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellFireScript : SpellParentScript { //Inherit from the spell parent script
	
	//List field for all enemies in the spell zone
	public List<GameObject> enemiesInZone = new List<GameObject>();

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Burn", .5f, .5f); //Invoke a repeating function to burn damage over time
		Invoke ("Die", stats.duration); //Invoke the die function after however many seconds the duration stat is
		this.GetComponent<SpriteRenderer> ().color = Color.red; //Change the color of the spell to red. DEFAULT
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
			enemiesInZone.Add (other.gameObject); //If so, add them to the list of enemies in the zone
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object exits the trigger area, check if it is an enemy
			enemiesInZone.Remove (other.gameObject); //If so, remove them from the list of enemies in the zone
		}
	}

	void Burn()
	{
		for (int ii = 0; ii < enemiesInZone.Count; ii++) { //Iterate through every enemy in the spell zone
						//Inflict base damage divided by four (base duration is two seconds, burn is called twice per second)
						//Then store the boolean if the enemy has died. Upgrading duration or damage will increase net damage dealt
						if (enemiesInZone [ii] != null) {
								enemiesInZone [ii].GetComponent<HealthScript> ().Decriment ((stats.damage / stats.duration) * 2f);
						}
						if (enemiesInZone [ii] == null) {
								enemiesInZone.Remove (enemiesInZone [ii]);
						}
				}
	}
}
