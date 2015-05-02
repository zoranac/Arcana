using UnityEngine;
using System.Collections;

public class BuildingEnter : MonoBehaviour {

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
            gameObject.renderer.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            gameObject.renderer.enabled = true;
        }

    }
}
