using UnityEngine;
using System.Collections;

public class SpellGUIScript : MonoBehaviour
{

    public SpellcastScript spellcaster;
    void OnGUI()
    {
        GUI.Label(new Rect(10f, 20f, 100f, 20f), spellcaster.myCombos[spellcaster.selectedSpell].shapeString);
		GUI.Label(new Rect(10f, 40f, 100f, 20f), spellcaster.myCombos[spellcaster.selectedSpell].elementString);
    }
}
