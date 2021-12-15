using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorText : MonoBehaviour
{
    private Text armorText;

    private int armor;

    // Start is called before the first frame update
    void Start()
    {
        armorText = GetComponent<Text>();
        armor = 0;
        armorText.text = "Armor: " + armor.ToString();
    }

    public void UpdateArmor(int armorValue) {
        armor = armorValue;
        armorText.text = "Armor: " + armor.ToString();
    }
}
