using UnityEngine;
using System.Collections;

public class State_Idle : State {

	public static State_Idle Idle = new State_Idle();

	public override void EnterState(GameObject Owner)
	{
		
	}
	public override void RunState(GameObject Owner)
	{
		
	}
	public override void ExitState(GameObject Owner, State nextState)
	{
		Owner.GetComponent<EnemyScript>().currentState = nextState;
		nextState.EnterState(Owner);
	}
}
