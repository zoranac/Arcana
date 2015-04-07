using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	Vector3 spawnPoint;
	float angle = 0;
	public float radius = 0;
	public float ChanceToSpawnChest;
	public float ChanceToSpawnEnemy;
	public GameObject Chest;
	public GameObject Enemy;
	public GameObject Exit;
	public int MaxEnemies;
	public int MaxChests;
	public int MaxExits;
	// Use this for initialization
	void Start () {
		ChanceToSpawnChest = ChanceToSpawnChest/100;
		ChanceToSpawnEnemy = ChanceToSpawnEnemy/100;
	}
	
	// Update is called once per frame
	void Update () {
		angle += ((Mathf.PI * 2)/24);
		if (angle>=(Mathf.PI * 2))
		{
			angle = 0;
		}
		spawnPoint = transform.position + new Vector3(Mathf.Cos(angle),Mathf.Sin(angle))*radius;

		float rand = Random.Range(0,100);
		if (rand <= ChanceToSpawnChest/1+(GameObject.FindGameObjectsWithTag("Chest").Length*2) && GameObject.FindGameObjectsWithTag("Chest").Length < MaxChests)
		{
			Instantiate(Chest,spawnPoint,transform.rotation);
		}
		rand = Random.Range(0,100);
		if (rand <= ChanceToSpawnEnemy/1+(GameObject.FindGameObjectsWithTag("Enemy").Length*2) && GameObject.FindGameObjectsWithTag("Enemy").Length < MaxEnemies)
		{
			Instantiate(Enemy,spawnPoint,transform.rotation);
		}
	}
}
