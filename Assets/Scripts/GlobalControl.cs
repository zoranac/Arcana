using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {
	public static GlobalControl globalControl;

	void Awake () {
		if(!globalControl) {
			globalControl = this;
			DontDestroyOnLoad(gameObject);
		}else
			Destroy(gameObject);
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
