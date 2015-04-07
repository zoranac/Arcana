using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

    public GameObject baddie; //The object that gets spawned

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Debug Purposes- X key spawns enemies
       // if (Input.GetKeyDown("x"))
        //{
        //    Spawn();
       // }
	
	}

    //Function that spawns gameobject
    public void Spawn(GameObject player)
    {
        GameObject Enemy = (GameObject)Instantiate(baddie, transform.position, transform.rotation); //Spawns enemy
        Enemy.gameObject.GetComponent<EnemyApproach>().player = player;
        
        WaveManager.enemiesAlive++; //Tracks an additional living enemy
    }
}
