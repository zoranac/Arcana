using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour {

	public virtual void EnterState(GameObject Owner)
	{

	}
	public virtual void RunState(GameObject Owner)
	{

	}
	public virtual void ExitState(GameObject Owner, State nextState)
	{

	}

}
