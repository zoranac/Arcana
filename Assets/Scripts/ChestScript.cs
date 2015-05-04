using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

    public int coinMin;

    public int coinMax;

    public int contents;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            contents = Random.Range(coinMin, coinMax);

            GlobalControl.globalControl.coins += contents;
            Destroy(gameObject);
        }

    }
}
