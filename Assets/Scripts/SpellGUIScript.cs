using UnityEngine;
using System.Collections;

public class SpellGUIScript : MonoBehaviour
{

    public SpellcastScript spellcaster;

    void OnGUI()
    {
        string message1 = string.Empty;
        switch (spellcaster.myCombos[spellcaster.selectedSpell].shape)
        {
            case 0:
                message1 = "Circle";
                break;
            case 1:
                message1 = "Line";
                break;
            case 2:
                message1 = "Cluster";
                break;
        }

        string message2 = string.Empty;
        switch (spellcaster.myCombos[spellcaster.selectedSpell].element)
        {
            case 0:
                message2 = "Fire";
                break;
            case 1:
                message2 = "Spark";
                break;
            case 2:
                message2 = "Ice";
                break;
            case 3:
                message2 = "Poison";
                break;
            case 4:
                message2 = "Wind";
                break;
            case 5:
                message2 = "Earth";
                break;
        }

        GUI.Label(new Rect(10f, 20f, 100f, 20f), message1);
        GUI.Label(new Rect(10f, 40f, 100f, 20f), message2);
    }
}
