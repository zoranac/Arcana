using UnityEngine;
using System.Collections;

public class SpellCombo{

	/* Shapes:	 0 = circle
	 			 1 = line
	 			 2 = spot
	   Elements: 0 = fire
	 			 1 = spark
	 			 2 = ice
	 			 3 = poison
	 			 4 = wind
	 			 5 = earth  */

	public int shape = 0;
	public int element = 0;

	public void InitializeValues(int _shape, int _element)
	{
		shape = _shape;
		element = _element;
	}
}
