using UnityEngine;
using System.Collections;
using InControl;

public class MinigameScript : MonoBehaviour {
	public GameObject Player;
	InputControl[] ButtonOrder = new InputControl[5];
	InputDevice inputDevice;
	int currentSpot = 0;
	bool running = false;
	// Use this for initialization
	void Start () {
		inputDevice = InputManager.ActiveDevice;
		Setup();
	}
	
	// Update is called once per frame
	void Update () {
		if (running)
		{
			Run();
			if (currentSpot >= 5)
			{
				//success
			}
		}
	}
	void Setup()
	{
		Player.GetComponent<PauseScript>().Pause();
		running = true;
		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0,4);
			if (rand == 0)
				ButtonOrder[i] = inputDevice.LeftBumper;
			if (rand == 1)
				ButtonOrder[i] = inputDevice.LeftTrigger;
			if (rand == 2)
				ButtonOrder[i] = inputDevice.RightBumper;
			if (rand == 3)
				ButtonOrder[i] = inputDevice.RightTrigger;
		}
	}
	void Run()
	{
		bool canPress = true;
		if  (currentSpot > 0)
		{
			if (ButtonOrder[currentSpot-1] == ButtonOrder[currentSpot])
			{
				if (ButtonOrder[currentSpot-1].IsPressed) 
				{
					canPress = false;
				}
				else
				{
					canPress = true;
				}
			}
		}
		if (ButtonOrder[currentSpot].IsPressed)
		{
			if (canPress)
			{
				currentSpot++;
			}
		}
		else if (inputDevice.AnyButton.IsPressed)
		{
			if  (currentSpot > 0)
			{
				if (!ButtonOrder[currentSpot-1].IsPressed) 
				{
					//Fail State
					running = false;
					Player.GetComponent<PauseScript>().UnPause();
				}
			}
			else
			{
				//Fail State
				running = false;
				Player.GetComponent<PauseScript>().UnPause();
			}
		}
	}
}
