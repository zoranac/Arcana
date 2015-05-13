using UnityEngine;
using System.Collections;

public class State_Seek : State {

	public static State_Seek Seek = new State_Seek();

	public override void EnterState(GameObject Owner)
	{
		
	}
	public override void RunState(GameObject Owner)
	{
		if (!Owner.GetComponent<EnemyScript>().overlappingEnemy)
		{
			if (Owner.GetComponent<EnemyScript>().moveAround())
			{
				
			}
			else
			{
				//			Vector3 vectorToTarget = target.transform.position - transform.position;
				//			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
				//			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				//			rigidbody2D.velocity = transform.up * 2;
				//rigidbody2D.AddForce((target.transform.position.normalized - transform.position.normalized) * 10);
				//rigidbody2D.velocity = (target.transform.position.normalized - transform.position.normalized) * 2;
				
				Vector3 vectorToTarget = Owner.GetComponent<EnemyScript>().target.transform.position - Owner.transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
				Owner.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
				
				Owner.transform.position = Vector3.MoveTowards(Owner.transform.position, Owner.GetComponent<EnemyScript>().targetPos, Owner.GetComponent<EnemyScript>().speed);
				
			}
		}
	}
	public override void ExitState(GameObject Owner, State nextState)
	{
		Owner.GetComponent<EnemyScript>().currentState = nextState;
		nextState.EnterState(Owner);
	}
}
