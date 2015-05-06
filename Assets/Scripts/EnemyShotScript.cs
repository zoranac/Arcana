using UnityEngine;
using System.Collections;

public class EnemyShotScript : MonoBehaviour {
	public float Speed;
	float damage;
	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = -(transform.position - GameObject.Find ("Player").transform.position).normalized *Speed;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Collider2D obj in Physics2D.OverlapPointAll(transform.position))
		{
			if (obj.tag == "Wall")
			{
				Destroy(gameObject);
			}
		}
	}
	public void SetDamage(float d)
	{
		damage = d;
	}
	void OnTriggerEnter2D(Collider2D otherObj)
	{
		if (otherObj.tag == "Player")
		{
			otherObj.gameObject.GetComponent<PlayerHealthScript>().Decriment(damage);
			GameObject.Find("Health").gameObject.GetComponent<HealthBarFlash>().FlashBar();
			Destroy(gameObject);
		}
	}

}
