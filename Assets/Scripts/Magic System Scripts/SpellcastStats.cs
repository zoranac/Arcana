using UnityEngine;
using System.Collections;

public class SpellcastStats{

//	public float frequency; //How fast the projectile should fire
//	public float size; //Size stat for various spells, most often the area of effect
//	public float duration; //Duration stat, how long a spell stays on the field or inflicts effects
//	public float damage; //Damage stat, base damage that spells use
	public float[] stats = new float[4]; //Frequency, size, duration, damage
	public bool[] atCap = new bool[4]; //array of booleans if the respective stat is at cap

	public SpellcastStats ReturnCopy()
	{
		SpellcastStats copy = new SpellcastStats ();
		copy.InitializeValues (stats [0], stats [1], stats [2], stats [3]);
		return copy;
	}

	public void InitializeValues(float _frequency, float _size, float _duration, float _damage)
	{
		stats [0] = _frequency;
		stats [1] = _size;
		stats [2] = _duration;
		stats [3] = _damage;
		CheckCaps ();
	}

	public void IncrementValue(int index)
	{
		if (atCap [index] == false)
						stats [index]++;
		CheckCaps ();
	}

	void CheckCaps()
	{
		for (int ii = 0; ii < stats.Length; ii++) {
						if (stats [ii] == 9)
								atCap [ii] = true;
				}
	}
}
