using UnityEngine;
using System.Collections;
using InControl;

public class MinigameScript : MonoBehaviour {
	public GameObject Player;
	public float ShowTimeDuration = .5f;
	InputControl[] ButtonOrder = new InputControl[5];
	InputDevice inputDevice;
	int currentSpot = 0;
	bool running = false;
	bool showedOrder = false;
	int showNumber = 0;
	float showtime = 0;

	// Use this for initialization
	void Start () {
		Player = transform.parent.gameObject;
		inputDevice = InputManager.ActiveDevice;
	}
	
	// Update is called once per frame
	void Update () {
		if (inputDevice.RightTrigger.IsPressed &&
		    inputDevice.LeftTrigger.IsPressed &&
		    inputDevice.RightBumper.IsPressed &&
		    inputDevice.LeftBumper.IsPressed &&
			GlobalControl.globalControl.Kills >= GlobalControl.globalControl.KillsToStartMinigame)
		{
			//And has enough kills;
			Setup();
		}
		if (running)
		{
			if (showedOrder)
				Run();
			else
				ShowOrder();
			if (currentSpot >= 5)
			{
				//success
			}
		}
	}
	public void Setup()
	{
		showedOrder = false;
		Player.GetComponent<PauseScript>().Pause();
		running = true;
		for (int i = 0; i < 5; i++)
		{
			int rand = Random.Range(0,4);
			if (rand == 0)
				ButtonOrder[i] = inputDevice.Action1;
			if (rand == 1)
				ButtonOrder[i] = inputDevice.Action2;
			if (rand == 2)
				ButtonOrder[i] = inputDevice.Action3;
			if (rand == 3)
				ButtonOrder[i] = inputDevice.Action4;
		}
		showtime = Time.time;
	}
	void ShowOrder()
	{
		if (Time.time >= showtime + ShowTimeDuration)
		{
			showNumber++;
			showtime = Time.time;
			gameObject.GetComponent<TextMesh>().text = showNumber.ToString();
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
