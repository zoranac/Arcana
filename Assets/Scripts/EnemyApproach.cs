using UnityEngine;
using System.Collections;

public class EnemyApproach : MonoBehaviour {

    public GameObject player; //The enemy will track towards this gameobject

    public float speed; //How quickly the enemy moves

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, step);
	
	}
}
