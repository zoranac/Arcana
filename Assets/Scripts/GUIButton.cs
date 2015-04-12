using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {
	public string controlName;
	public string text;
	public Rect rect;

	public void SetValues(string _controlName, string _text)
	{
		controlName = _controlName;
		text = _text;
	}
	public void SetRect(Rect _rect)
	{
		rect = new Rect(_rect);
	}
}
