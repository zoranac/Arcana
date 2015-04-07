using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public static int enemiesAlive;


    public GameObject[] SpawnPoints; //An array that holds all the potential Spawn Points

    int number; //A random number for detirmining spawnpoints

    int enemies; //A random number for detirmining how many enemies are spawned.

	// Use this for initialization
	void Start () {

        enemiesAlive = 2;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (enemiesAlive == 0)
        {
           
            enemies = Random.Range(1, 4);// Decide how many enemies to spawn

            //Spawn that many enemies
            for (int i = 0; i < enemies; i++)
            {
                number = Random.Range(0, 3);
                SpawnPoints[number].gameObject.GetComponent<EnemySpawn>().Spawn(this.gameObject);
            }

            

        }
	
	}
}
