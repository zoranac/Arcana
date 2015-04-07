using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool destroy = true;
		foreach (Collider2D obj in Physics2D.OverlapPointAll(transform.position))
		{
			if (obj.tag == "PlayerSpace")
			{
				destroy = false;
				break;
			}
		}
		if (destroy)
		{
			Destroy(gameObject);
		}
	}
}
