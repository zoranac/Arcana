using UnityEngine;
using System.Collections;

public class SpellCombo {

	public int shape;
	public int element;
	public string shapeString;
	public string elementString;

	public void InitializeValues(int _shape, int _element)
	{
		shape = _shape;
		element = _element;

		switch (shape)
		{
		case 0:
			shapeString = "Circle";
			break;
		case 1:
			shapeString = "Line";
			break;
		case 2:
			shapeString = "Cluster";
			break;
		}
		
		switch (element)
		{
		case 0:
			elementString = "Fire";
			break;
		case 1:
			elementString = "Spark";
			break;
		case 2:
			elementString = "Ice";
			break;
		case 3:
			elementString = "Poison";
			break;
		case 4:
			elementString = "Wind";
			break;
		case 5:
			elementString = "Earth";
			break;
		}
	}
}
