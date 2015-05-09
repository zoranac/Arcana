using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

	// Use this for initialization
	void Start () {

        gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Takes you to the menu for now, later will move player to next area!
            Application.LoadLevel(0);
        }

    }
}
