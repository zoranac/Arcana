using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public static int enemiesAlive;

    public int enemyDebug;
    public int waveDebug;


    public GameObject[] SpawnPoints; //An array that holds all the potential Spawn Points

    int number; //A random number for detirmining spawnpoints

    int enemies; //A random number for detirmining how many enemies are spawned.

	// Use this for initialization
	void Start () {

        enemiesAlive = 2;
	
	}
	
	// Update is called once per frame
	void Update () {

        enemyDebug = enemiesAlive; //enemyDebug is a version of the enemiesAlive variable which shows up in the
                                   //console window

        if (enemiesAlive <= 0)
        {
           
            enemies = Random.Range(2, 6);// Decide how many enemies to spawn

            waveDebug = enemies; //waveDebug shows the number of enemies that SHOULD be spawning in this wave

            //Spawn that many enemies
            for (int i = 0; i < enemies; i++)
            {
                number = Random.Range(0, 3);
                SpawnPoints[number].GetComponent<EnemySpawn>().Spawn(this.gameObject);
            }

            

        }
	
	}
}
