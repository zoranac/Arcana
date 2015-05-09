using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public GameObject statManager;

	public float HP = 10; //Health of the enemy


    public void Start()
    {
        statManager = GameObject.Find("StatManager");
    }

	//Function to inflict damage
	//Used for any logic like armor or conditional health decrimenting
	public void Decriment(float damage)
	{
		HP -= damage; //Subtract damage from health
		if (HP <= 0) { //If the health is at or below zero
						Die ();
				}
	}

	public void Die()
	{
		GlobalControl.globalControl.Kills++;
        statManager.GetComponent<StatManager>().Special(10);
		GlobalControl.globalControl.coins += Random.Range (1, 2); //Gives coins on enemy death
		Destroy (this.gameObject); //Destroy the enemy
	}
}
