using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {


    public int level;

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
            switch (level)
            {
                case 0:
                    Application.LoadLevel(0);
                    break;

                case 1:

                    Application.LoadLevel(3);

                    break;

                case 2:

                    Application.LoadLevel(4);

                    break;


                case 3:

                    Application.LoadLevel(5);

                    break;

                case 4:

                    Application.LoadLevel(0);

                    break;
            }
        }

    }
}
