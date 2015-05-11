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
	public Vector3 targetPos = Vector3.zero;
    public GameObject layout;
    public float damage;
    public float rangeDamage;
	bool stuck = false;
	public float speedMax = .025f; //ADDED
	public float speedMin = .001f; //ADDED
	public bool slowed = false; //ADDED
	float speed = .02f;
	public bool Ranged = false;
	public float ShotInterval = 0;
	float shotTime = 0;
	public GameObject RangedShotPrefab;
	bool flee = false;
	public float CriticalHP;
	enum State
	{
		Idle,
		Moving,
		Wander,
		Attacking,
		Fleeing
	}

	State currentState = State.Idle;
	bool overlappingEnemy = false;
	// Use this for initialization
	void Start () {

        layout = GameObject.Find("Health");

	}
	
	// Update is called once per frame
	void FixedUpdate () { //CHANGED
		speed = Random.Range(speedMin,speedMax);
		playerInSightRange = false;
		playerInAttackRange = false;
		overlappingEnemy = false;


		foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,SightRange))
		{
			if (obj.tag == "Player")
			{
				bool wallHit = false;
				foreach (RaycastHit2D obj2 in Physics2D.LinecastAll(transform.position,obj.transform.position))
				{
					if (obj2.collider.tag == "Wall")
					{
						wallHit = true;
						break;
					}
				}
				if (!wallHit)
				{
					playerInSightRange = true;
					target = obj.gameObject;
					targetPos = target.transform.position;
					speed = Random.Range((obj.transform.position - transform.position).magnitude*speedMin,(obj.transform.position - transform.position).magnitude*speedMax); //CHANGED
				}
			}

		}
		foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,AttackRange))
		{
			if (obj.tag == "Player")
			{
				playerInAttackRange = true;
				target = obj.gameObject;
				targetPos = target.transform.position;
			}

		}

		foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,AttackRange/1.5f))
		{
			if (obj.tag == "Player")
			{
				//transform.position = Vector3.MoveTowards(transform.position,-(obj.transform.position)*4, speed);
				overlappingEnemy = true;
				break;
			}
		}
		if (!overlappingEnemy)
		{
			foreach (Collider2D obj in Physics2D.OverlapCircleAll(transform.position,CollisionSpacerRange))
			{
				if (obj.tag == "Enemy"&& obj.gameObject != gameObject || obj.tag == "Player")
				{
					targetPos = -(obj.transform.position)*2;
					if (transform.position.x > obj.transform.position.x)
					{
						targetPos.x = transform.position.x + obj.transform.position.x;
					}
					else
					{
						targetPos.x = transform.position.x - obj.transform.position.x;
					}

					if (transform.position.y > obj.transform.position.y)
					{
						targetPos.y = transform.position.y + obj.transform.position.y;
					}
					else
					{
						targetPos.y = transform.position.y - obj.transform.position.y;
					}
					transform.position = Vector3.MoveTowards(transform.position,targetPos*2, speed);
					overlappingEnemy = true;
					//rigidbody2D.AddForce();
					break;
				}
			}
		}
//		if (overlappingEnemy)
//		{
//			//currentState = State.Idle;
//		}
//		else 

		if (playerInSightRange && flee)
		{
			currentState = State.Fleeing;
		}
		else if (flee)
		{
			currentState = State.Wander;
		}
		else if (playerInAttackRange)
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
			currentState = State.Wander;
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
		if (currentState == State.Wander)
		{
			Wander();
		}
		if (currentState == State.Fleeing)
		{
			Flee(target);
		}
		if (currentState == State.Idle)
		{

		}
		Vector3 pos = transform.position;
		pos.z = 0;
		transform.position = pos;

	}
	public void FleeTest(float health)
	{

		if (health <= CriticalHP)
		{
			int rand = Random.Range(0,2);
			print (rand);
			if (rand == 0)
				SetFlee(true);
		}
	}
	public void SetFlee(bool b)
	{
			flee = b;
	}
	void Flee(GameObject fleeTarget)
	{
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
				
				transform.position = Vector3.MoveTowards(transform.position, -targetPos, speed/2);
				
			}
		}
	}
	void attack(GameObject attackTarget)
	{
		Vector3 vectorToTarget = target.transform.position - transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		rigidbody2D.velocity = Vector2.zero;
		if (!Ranged)
		{
      	  	target.gameObject.GetComponent<PlayerHealthScript>().Decriment(damage);
        	layout.gameObject.GetComponent<HealthBarFlash>().FlashBar();
		}
		else
		{
			if (Time.time >= shotTime + ShotInterval)
			{
				GameObject temp = (GameObject)Instantiate(RangedShotPrefab,transform.position,transform.rotation);
                //Setting Damage to a seperate public variable- shouldn't be the same as melee damage since melee damage is incurred much more frequently.
				temp.GetComponent<EnemyShotScript>().SetDamage(rangeDamage);
				shotTime = Time.time;
			}
		}
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

				transform.position = Vector3.MoveTowards(transform.position, targetPos, speed);

			}
		}
	}
	void Wander()
	{
		if (moveAround())
		{
			//transform.position = Vector3.MoveTowards(transform.position, targetPos, .03f);
		}
		else
		{
			//hit = false;
			targetPos = targetPos.normalized;
			targetPos.x = Random.Range(targetPos.x - .25f,targetPos.x + .25f);
			targetPos.y = Random.Range(targetPos.y - .25f,targetPos.y + .25f);
			targetPos *= 1000;
			targetPos.z = -1;
			
			transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*3);
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
