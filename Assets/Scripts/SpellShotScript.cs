using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellShotScript : MonoBehaviour {
	
	public SpellcastStats stats; //Handle to the player's spellcast script to access its variables
	public SpellCombo myCombo;

	//Prefab for the specific rune shape selected
	public GameObject circleShape; //Prefab of a circle
	public GameObject lineShape; //Prefab of a line
	public GameObject spotShape; //prefab of the beam

	void Awake()
	{
		rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous; //Set the shot to detect collisions
		Invoke ("Die", 20f);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.gameObject.tag == "Enemy") { //On collision, if the object hit is an enemy, begin logic
						List<GameObject> mySpell = new List<GameObject> (); //Create a handle to a new game object

						switch (myCombo.shape) { //Conditional branch based on what shape the current spell is
						case 0: //If circle
								//Instantiate the circle prefab at the position and rotation of the enemy and set handle of the new object to mySpell
								mySpell.Add ((GameObject)Instantiate (circleShape, other.transform.position, other.transform.rotation));
								break;
						case 1: //If line
								//Instantiate a line prefab at the position of the enemy and zero rotation
								mySpell.Add ((GameObject)Instantiate (lineShape, other.transform.position, new Quaternion (0f, 0f, 0f, 0f)));
								break;
						case 2: //If spot
								for (int ii = 0; ii < 4; ii++) {
										Vector3 rand = new Vector3 (Random.Range (-1f, 1f), Random.Range (-1f, 1f), Random.Range (-1f, 1f));
										mySpell.Add ((GameObject)Instantiate (spotShape, other.transform.position + rand, other.transform.rotation));
								}
								break;
						}

						switch (myCombo.element) { //Conditional branch based on what the current element is
						case 0: //If fire
								foreach (GameObject spell in mySpell) {
										spell.AddComponent<SpellFireScript> (); //Add a fire script component to the spell
								}
								break;
						case 1: //If spark
								//Add a spark script to the spell
								foreach (GameObject spell in mySpell) {
										spell.AddComponent<SpellShockScript> (); //Add a fire script component to the spell
								}
								break;
						case 2: //If ice
							//Add an ice script to the spell
							foreach (GameObject spell in mySpell) {
								spell.AddComponent<SpellFrostScript> (); //Add a fire script component to the spell
							}
							break;
						case 3: //If poison
							//Add a poison script to the spell
							foreach (GameObject spell in mySpell) {
								spell.AddComponent<SpellPoisonScript> (); //Add a fire script component to the spell
							}
							break;
						case 4: //If wind
							//Add a wind script to the spell
							foreach (GameObject spell in mySpell) {
								spell.AddComponent<SpellWindScript> (); //Add a fire script component to the spell
							}
							break;
						case 5: //If earth
							//Add an earth script to the spell
							foreach (GameObject spell in mySpell) {
								spell.AddComponent<SpellEarthScript> (); //Add a fire script component to the spell
							}
							break;
						}

						foreach (GameObject spell in mySpell) {
								//Change the scale of the spell to the size stat of the player
								spell.transform.localScale = new Vector3 (stats.size, stats.size, stats.size);
								//Use inheritance to initialize the stat values of damage and duration on the spell script assigned to the spell.
								//All spell scripts inherit from SpellParentScript and inherit the InitializeValues function, so polymorphism works here.
								spell.GetComponent<SpellParentScript> ().stats = stats.ReturnCopy ();
						}

						//Destroy the spell shot object
						Destroy (this.gameObject);
				}
        else//If it collides with a non-enemy
        {
            //Destroy spell shot with no effect
            Destroy(this.gameObject);
        }
	}

	void Die()
	{
		Destroy (this.gameObject);
	}
}
