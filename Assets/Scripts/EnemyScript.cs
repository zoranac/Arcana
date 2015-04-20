using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public float SightRange;
	GameObject sightRangeObject;
	bool playerInSightRange;

	public float AttackRange;
	GameObject attackRangeObject;
	bool playerInAttackRange;

	public float CollisionSpacerRange; 

	GameObject collisionObject;

	public bool HitTop;
	public bool HitBottom;
	public bool HitRight;
	public bool HitLeft;

	public GameObject target;

	bool stuck = false;
	public float speedMax = .025f; //ADDED
	public float speedMin = .001f; //ADDED
	public bool slowed = false; //ADDED
	float speed = .02f;
	enum State
	{
		Idle,
		Moving,
		Attacking
	}

	State currentState = State.Idle;
	bool overlappingEnemy = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () { //CHANGED
		speed = Random.Range(.01f,.03f);
		playerInSightRange = false;
		playerInAttackRange = false;
		overlappingEnemy = false;


		foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,SightRange))
		{
			if (obj.tag == "Player")
			{
				playerInSightRange = true;
				target = obj.gameObject;
				speed = Random.Range((obj.transform.position - transform.position).magnitude*speedMin,(obj.transform.position - transform.position).magnitude*speedMax); //CHANGED
			}
		}
		foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,AttackRange))
		{
			if (obj.tag == "Player")
			{
				playerInAttackRange = true;
				target = obj.gameObject;
			}

		}

		foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,CollisionSpacerRange))
		{
			if (obj.tag == "Enemy"&& obj.gameObject != gameObject || obj.tag == "Player")
			{
				transform.position = Vector3.MoveTowards(transform.position,-(obj.transform.position)*2, speed);
				overlappingEnemy = true;
				//rigidbody2D.AddForce();
			}
		}

		if (playerInAttackRange)
		{
			currentState = State.Attacking;
		
		}
		else if (playerInSightRange)
		{
			currentState = State.Moving;
			//transform.Rotate(Vector3.zero);
		}
		else
		{
			currentState = State.Idle;
			target = null;
		}

		if (currentState == State.Attacking)
		{
			attack(target);
		}
		
		if (currentState == State.Moving)
		{
			move(target);
		}
		
		if (currentState == State.Idle)
		{
			
		}
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;

	}

	void attack(GameObject attackTarget)
	{
		Vector3 vectorToTarget = target.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rigidbody2D.velocity = Vector2.zero;
	}
	void move(GameObject moveTarget)
	{
		//rigidbody2D.velocity = Vector2.MoveTowards(transform.position,moveTarget.transform.position,100) * .5f;
		if (!overlappingEnemy)
		{
			if (moveAround())
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

				Vector3 vectorToTarget = target.transform.position - transform.position;
				float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

				transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
			}
		}
	}
	bool moveAround()
	{

		if (HitTop)
		{
			if (target.transform.position.x > transform.position.x)
			{
				if (target.transform.position.y > transform.position.y)
				{

				}
				else
				{

				}
				if (HitRight)
				{ 
					transform.position = Vector3.MoveTowards(transform.position, -transform.up, speed);
				}
				else if (HitLeft)
				{
					transform.position = Vector3.MoveTowards(transform.position, -transform.up, speed);
				}
				else
				{
					transform.position = Vector3.MoveTowards(transform.position, -transform.right, speed);
				}
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, transform.right, speed);
			}

			return true;
		}
		else if (HitBottom)
		{
			if (target.transform.position.x > transform.position.x)
			{
				if (HitRight)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.up, speed);
				}
				else if (HitLeft)
				{
					transform.position = Vector3.MoveTowards(transform.position, transform.up, speed);
				}
				else
				{
					transform.position = Vector3.MoveTowards(transform.position, -transform.right, speed);
				}
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, transform.right, speed);
			}

			return true;
		}
		if (HitRight)
		{
			if (target.transform.position.y > transform.position.y)
			{
				transform.position = Vector3.MoveTowards(transform.position, transform.up, speed);
				
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, -transform.up, speed);
			}

			return true;
		}
		else if (HitLeft)
		{
			if (target.transform.position.y > transform.position.y)
			{
				transform.position = Vector3.MoveTowards(transform.position, transform.up, speed);
				
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, -transform.up, speed);
			}

			return true;
		}

		return false;
	}

}
