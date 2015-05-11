using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public GameObject statManager;
    public GameObject globalControl;

	public float HP = 10; //Health of the enemy

    public float specialInc; //The incriment by which the enemy's death increases the special bar.

    public int killNum;

    public void Start()
    {
        statManager = GameObject.Find("StatManager");
        globalControl = GameObject.Find("GlobalControl");

        killNum = globalControl.GetComponent<GlobalControl>().KillsToStartMinigame;

        specialInc = 211 / (killNum + 1);


    }

	//Function to inflict damage
	//Used for any logic like armor or conditional health decrimenting
	public void Decriment(float damage)
	{
		if (gameObject.tag == "Enemy")
		{
			gameObject.GetComponent<EnemyScript>().FleeTest(HP);
		}
		HP -= damage; //Subtract damage from health
		if (HP <= 0) { //If the health is at or below zero
						Die ();
				}
	}

	public void Die()
	{
		GlobalControl.globalControl.Kills++;
        statManager.GetComponent<StatManager>().Special(specialInc);
		GlobalControl.globalControl.coins += Random.Range (1, 2); //Gives coins on enemy death
		Destroy (this.gameObject); //Destroy the enemy
	}
}
