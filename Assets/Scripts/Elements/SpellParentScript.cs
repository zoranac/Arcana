using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellParentScript : MonoBehaviour {

	public SpellcastStats stats;
	public GameObject myWand;

	//COPY TO CHILDREN
//	void Start () {
//		Invoke ("Die", stats.duration); //Invoke the die function after however many seconds the duration stat is
//	}
//
//	void OnTriggerEnter2D(Collider2D other)
//	{
//		if (other.gameObject.tag == "Enemy") { //When an object enters the trigger area, check if it is an enemy
//		}
//	}
//	
//	void OnTriggerExit2D(Collider2D other)
//	{
//		if (other.gameObject.tag == "Enemy") { //When an object exits the trigger area, check if it is an enemy
//		}
//	}

	void Die()
	{
		Destroy (this.gameObject); //Destroy the spell object
	}
}
