using UnityEngine;
using System.Collections;

public class State_Attack : State {

	public static State_Attack Attack = new State_Attack();

	public override void EnterState(GameObject Owner)
	{
		
	}
	public override void RunState(GameObject Owner)
	{
		Vector3 vectorToTarget = Owner.GetComponent<EnemyScript>().target.transform.position - Owner.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		Owner.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		Owner.rigidbody2D.velocity = Vector2.zero;
		if (!Owner.GetComponent<EnemyScript>().Ranged)
		{
			Owner.GetComponent<EnemyScript>().target.gameObject.GetComponent<PlayerHealthScript>().Decriment(Owner.GetComponent<EnemyScript>().damage);
			Owner.GetComponent<EnemyScript>().layout.gameObject.GetComponent<HealthBarFlash>().FlashBar();
		}
		else
		{
			if (Time.time >= Owner.GetComponent<EnemyScript>().shotTime + Owner.GetComponent<EnemyScript>().ShotInterval)
			{
				GameObject temp = (GameObject)Instantiate(Owner.GetComponent<EnemyScript>().RangedShotPrefab,Owner.transform.position,Owner.transform.rotation);
				//Setting Damage to a seperate public variable- shouldn't be the same as melee damage since melee damage is incurred much more frequently.
				temp.GetComponent<EnemyShotScript>().SetDamage(Owner.GetComponent<EnemyScript>().rangeDamage);
				Owner.GetComponent<EnemyScript>().shotTime = Time.time;
			}
		}
	}
	public override void ExitState(GameObject Owner, State nextState)
	{
		Owner.GetComponent<EnemyScript>().currentState = nextState;
		nextState.EnterState(Owner);
	}
}
