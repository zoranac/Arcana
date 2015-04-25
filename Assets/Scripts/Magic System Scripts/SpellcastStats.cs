using UnityEngine;
using System.Collections;

public class SpellcastStats{

	public float frequency; //How fast the projectile should fire
	public float size; //Size stat for various spells, most often the area of effect
	public float duration; //Duration stat, how long a spell stays on the field or inflicts effects
	public float damage; //Damage stat, base damage that spells use
	public bool[] atCap = new bool[4]; //array of booleans if the respective stat is at cap

	public SpellcastStats ReturnCopy()
	{
		SpellcastStats copy = new SpellcastStats ();
		copy.frequency = frequency;
		copy.size = size;
		copy.duration = duration;
		copy.damage = damage;

		return copy;
	}

	public void InitializeValues(float _frequency, float _size, float _duration, float _damage)
	{
		frequency = _frequency;
		size = _size;
		duration = _duration;
		damage = _damage;
		CheckCaps ();
	}

	public void IncrementValue(int index)
	{
		if (atCap [index] == false) {
			if (index==0)
				frequency++;
			else if (index==1)
				size++;
			else if (index==2)
				duration++;
			else if (index==3)
				damage++;
				}
		CheckCaps ();
	}

	void CheckCaps()
	{
		if (frequency == 9)
						atCap [0] = true;
				else if (size == 9)
						atCap [1] = true;
				else if (duration == 9)
						atCap [2] = true;
				else if (damage == 9)
						atCap [2] = true;
	}
}
