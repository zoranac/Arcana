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
    public void Spawn(GameObject _player)
    {

        print("1");
        Invoke("UpEnemyCount", .05f);

        GameObject Enemy = (GameObject)Instantiate(baddie, transform.position, transform.rotation); //Spawns enemy
        print("2");
        Enemy.GetComponent<EnemyApproach>().player = _player;
        print("3");
        print("4");
        
    }

    public void UpEnemyCount()
{
    WaveManager.enemiesAlive++; //Tracks an additional living enemy
}
}
