using UnityEngine;
using System.Collections;

public class GlobalControl : MonoBehaviour {
	public static GlobalControl globalControl;
	public int coins;
	public int KillsToStartMinigame = 20;
	public int Kills = 0;
	public SpellcastStats stats;

	void Awake () {
		if(!globalControl) {
			//Initialize globalcontrol
			globalControl = this;
			DontDestroyOnLoad(gameObject);
			//Initialize variables on game start
			coins = 0;
			stats = new SpellcastStats();
			stats.InitializeValues(1, 1, 1, 1);
			PauseScript.unlockedShapes = new bool[]{true, false, false};
			PauseScript.unlockedElements = new bool[]{true, false, false, false, false, false};
		}else
			Destroy(gameObject);
	}
}
