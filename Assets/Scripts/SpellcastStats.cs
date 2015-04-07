using UnityEngine;
using System.Collections;

public class SpellcastStats{

	public float frequency; //How fast the projectile should fire
	public float size; //Size stat for various spells, most often the area of effect
	public float duration; //Duration stat, how long a spell stays on the field or inflicts effects
	public float damage; //Damage stat, base damage that spells use

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
	}
}
