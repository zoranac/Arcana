using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellFrostScript : SpellParentScript {

	public List<GameObject> enemiesDamaged = new List<GameObject>();
	public List<GameObject> enemiesInZone = new List<GameObject>();

	void Start () {
		Invoke ("Die", stats.stats[2]); //Invoke the die function after however many seconds the duration stat is
		this.GetComponent<SpriteRenderer> ().color = Color.blue; //Change the color of the spell to blue. DEFAULT
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
			if (enemiesDamaged.Contains(other.gameObject) == false) //Check if this enemy has been damaged by this spell before
			{
				enemiesDamaged.Add(other.gameObject); //If not, add it to the list of damaged enemies so none get hit multiple times
				other.gameObject.GetComponent<HealthScript>().Decriment(stats.stats[3] / 2f); //Deal the flat damage stat 
				CleanUp();
			}
			enemiesInZone.Add (other.gameObject); //If so, add them to the list of enemies in the zone
			SpeedToggle(other.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy") { //When an object exits the trigger area, check if it is an enemy
			enemiesInZone.Remove (other.gameObject); //If so, remove them from the list of enemies in the zone
			SpeedToggle(other.gameObject);
		}
	}

	void CleanUp()
	{
		for (int ii = 0; ii < enemiesInZone.Count; ii++) { //Iterate through every enemy in the spell zone
			if (enemiesInZone [ii] == null) {
				enemiesInZone.Remove (enemiesInZone [ii]);
			}
		}
		for (int ii = 0; ii < enemiesDamaged.Count; ii++) { //Iterate through every enemy in the spell zone
			if (enemiesDamaged [ii] == null) {
				enemiesDamaged.Remove (enemiesDamaged [ii]);
			}
		}
	}

	void SpeedToggle(GameObject enemy)
	{
		EnemyScript enemyScript = enemy.GetComponent<EnemyScript> ();
		if (enemyScript.slowed) {
						enemyScript.speedMax *= 2.0f;
						enemyScript.speedMin *= 2.0f;
				} else {
						enemyScript.speedMax *= 0.5f;
						enemyScript.speedMin *= 0.5f;
				}
		enemyScript.slowed = !enemyScript.slowed;
	}

	protected override void Die ()
	{
		foreach (GameObject enemy in enemiesInZone) {
						SpeedToggle (enemy);
				}
		base.Die ();
	}
}
