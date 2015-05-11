using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	Vector3 spawnPoint;
	float angle = 0;
	public float radius = 0;
	public float ChanceToSpawnChest;
	public float ChanceToSpawnEnemy;
	public GameObject Chest;

    //Common Enemy (50%)
	public GameObject Enemy;
    //Common Enemy (40%)
    public GameObject Enemy2;
    //Rare Enemy  (9%)
    public GameObject Enemy3;
    //Special Enemy (1%)
    public GameObject EnemySpec;

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
	void FixedUpdate () {
		angle += ((Mathf.PI * 2)/24);
		if (angle>=(Mathf.PI * 2))
		{
			angle = 0;
		}
		spawnPoint = transform.position + new Vector3(Mathf.Cos(angle),Mathf.Sin(angle))*radius;

		float rand = Random.Range(0,100);
		if (rand <= ChanceToSpawnChest/1+(GameObject.FindGameObjectsWithTag("Chest").Length*2) && GameObject.FindGameObjectsWithTag("Chest").Length < MaxChests)
		{
			bool spawn = false;
			foreach (Collider2D obj in Physics2D.OverlapPointAll(spawnPoint))
			{
				if (obj.tag == "Floor")
				{
					spawn = true;
				}
				if (obj.tag == "Wall")
				{
					spawn = false;
					break;
				}
			}
			if (spawn)
				Instantiate(Chest,spawnPoint,transform.rotation);
		}
		rand = Random.Range(0,100);
		if (rand <= ChanceToSpawnEnemy/1+(GameObject.FindGameObjectsWithTag("Enemy").Length*2) && GameObject.FindGameObjectsWithTag("Enemy").Length < MaxEnemies)
		{
			bool spawn = false;
			foreach (Collider2D obj in Physics2D.OverlapPointAll(spawnPoint))
			{
				if (obj.tag == "Floor")
				{
					spawn = true;
				}
				if (obj.tag == "Wall")
				{
					spawn = false;
					break;
				}
			}
            if (spawn)
            {
                rand = Random.Range(0, 100);

                if (rand > 99)
                Instantiate(EnemySpec, spawnPoint, transform.rotation);
                else if (rand > 90)
                Instantiate(Enemy3, spawnPoint, transform.rotation);
                else if (rand > 50)
                Instantiate(Enemy2, spawnPoint, transform.rotation);
                else if (rand >= 0)
                Instantiate(Enemy, spawnPoint, transform.rotation);

                
            }
		}
	}
}
