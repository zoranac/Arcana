using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellEarthScript : SpellParentScript {

	public List<GameObject> enemiesDamaged = new List<GameObject>();
	public List<GameObject> enemiesInZone = new List<GameObject>();

	void Start () {
		Invoke ("Die", stats.duration); //Invoke the die function after however many seconds the duration stat is
		this.GetComponent<SpriteRenderer> ().color = Color.green; //Change the color of the spell to blue. DEFAULT
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
			enemiesInZone.Add (other.gameObject); //If so, add them to the list of enemies in the zone
			if (enemiesDamaged.Contains(other.gameObject) == false) //Check if this enemy has been damaged by this spell before
			{
				enemiesDamaged.Add(other.gameObject); //If not, add it to the list of damaged enemies so none get hit multiple times
				other.gameObject.GetComponent<HealthScript>().Decriment(stats.damage / 2f); //Deal the flat damage stat 
			}
			Vector2 direction =  other.gameObject.transform.position - this.transform.position;
			direction = Vector2.ClampMagnitude(direction, 1f);
			other.gameObject.rigidbody2D.AddForce(direction * 1000f);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object exits the trigger area, check if it is an enemy
			enemiesInZone.Remove (other.gameObject); //If so, remove them from the list of enemies in the zone
		}
	}
	
	void Update()
	{
		foreach (GameObject enemy in enemiesInZone) {

		}
	}
}
