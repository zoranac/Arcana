using UnityEngine;
using System.Collections;
using InControl;

public class MinigameScript : MonoBehaviour {
	public GameObject Player;

    //Black Hole to next floor, might have to just set it manually in-window for now
    public GameObject BlackHole;
    public GameObject[] GuysToBreak;

	public float ShowTimeDurationInFrames;
	InputControl[] ButtonOrder = new InputControl[5];
	InputDevice inputDevice;
	int currentSpot = 0;
	bool running = false;
	bool showedOrder = false;
	int showNumber = 0;
	float showtime = 0;
	float UpdateTimer = 0f;
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
			print ("setup Minigame");
			Setup();
		}
		if (running)
		{
			UpdateTimer++;
			if (currentSpot >= 5)
			{
				//success
				print ("success!");
                GuysToBreak = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject guy in GuysToBreak)
                {
                    Destroy(guy.gameObject);
                }
                BlackHole.gameObject.SetActive(true);


				gameObject.GetComponent<TextMesh>().text = "O";
				EndMinigame();
			}
			else if (showedOrder)
				Run();
			else
				ShowOrder();

		}
	}
	void EndMinigame()
	{
		currentSpot = 0;
		running = false;
		showedOrder = false;
		showNumber = 0;
		showtime = 0;
		UpdateTimer = 0f;
		gameObject.GetComponent<TextMesh>().text = "";
		Player.GetComponent<PauseScript>().UnPause();
	}
	public void Setup()
	{
		UpdateTimer = 0;
		showedOrder = false;
		Player.GetComponent<PauseScript>().Pause();
		running = true;
		InputControl lastInput = null;
		for (int i = 0; i < 5; i++)
		{
			if (i > 0)
			{
				do{
					int rand = Random.Range(0,4);
					if (rand == 0)
						ButtonOrder[i] = inputDevice.Action1;
					if (rand == 1)
						ButtonOrder[i] = inputDevice.Action2;
					if (rand == 2)
						ButtonOrder[i] = inputDevice.Action3;
					if (rand == 3)
						ButtonOrder[i] = inputDevice.Action4;
				}while (ButtonOrder[i] == lastInput);
			}
			else{
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
			lastInput = ButtonOrder[i];
		}
		showtime = UpdateTimer;
	}
	void ShowOrder()
	{

		if (UpdateTimer >= showtime + ShowTimeDurationInFrames)
		{
			if (showNumber >= ButtonOrder.Length)
			{
				showedOrder = true;
				gameObject.GetComponent<TextMesh>().text = "?";
			}
			else{
				showtime = UpdateTimer;
				gameObject.GetComponent<TextMesh>().text = ButtonOrder[showNumber].ToString();
				showNumber++;
			}
		}
	}
	void Run()
	{
		bool canPress = true;
		if  (currentSpot > 0)
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
        if (currentSpot < 5)
        {
            if (ButtonOrder[currentSpot].IsPressed)
            {
                if (canPress)
                {
                    currentSpot++;
                }
            }
            else if (inputDevice.AnyButton.IsPressed)
            {
                if (currentSpot > 0)
                {
                    if (!ButtonOrder[currentSpot - 1].IsPressed)
                    {
                        //Fail State
                        print("failed :(");
                        gameObject.GetComponent<TextMesh>().text = "X";
                        EndMinigame();
                    }
                }
                else
                {
                    //Fail State
                    print("failed :(");
                    gameObject.GetComponent<TextMesh>().text = "X";
                    EndMinigame();
                }
            }
        }
	}
}
