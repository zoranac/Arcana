using UnityEngine;
using System.Collections;

public class GUIButton{
	public string controlName;
	public string text;
	public Rect rect;

	public GUIButton(string _controlName, string _text)
	{
		controlName = _controlName;
		text = _text;
	}
	public void SetRect(Rect _rect)
	{
		rect = new Rect(_rect);
	}
}
