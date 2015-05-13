using UnityEngine;
using System.Collections;

public class State_Wander : State {

	public static State_Wander Wander = new State_Wander();

	public override void EnterState(GameObject Owner)
	{
		
	}
	public override void RunState(GameObject Owner)
	{
		if (Owner.GetComponent<EnemyScript>().moveAround())
		{
			//transform.position = Vector3.MoveTowards(transform.position, targetPos, .03f);
		}
		else
		{
			//hit = false;
			Owner.GetComponent<EnemyScript>().targetPos = Owner.GetComponent<EnemyScript>().targetPos.normalized;
			Owner.GetComponent<EnemyScript>().targetPos.x = Random.Range(Owner.GetComponent<EnemyScript>().targetPos.x - .25f,Owner.GetComponent<EnemyScript>().targetPos.x + .25f);
			Owner.GetComponent<EnemyScript>().targetPos.y = Random.Range(Owner.GetComponent<EnemyScript>().targetPos.y - .25f,Owner.GetComponent<EnemyScript>().targetPos.y + .25f);
			Owner.GetComponent<EnemyScript>().targetPos *= 1000;
			Owner.GetComponent<EnemyScript>().targetPos.z = -1;
			
			Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, Owner.GetComponent<EnemyScript>().targetPos, Owner.GetComponent<EnemyScript>().speed*3);
		}
	}
	public override void ExitState(GameObject Owner, State nextState)
	{
		Owner.GetComponent<EnemyScript>().currentState = nextState;
		nextState.EnterState(Owner);
	}
}
