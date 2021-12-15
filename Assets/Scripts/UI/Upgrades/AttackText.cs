using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackText : MonoBehaviour
{
    private Text attackText;

    private int attack;

    // Start is called before the first frame update
    void Start()
    {
        attackText = GetComponent<Text>();
        attack = 1;
        attackText.text = "Attack: " + attack.ToString();
    }

    public void UpdateAttack(int attackValue) {
        attack = attackValue;
        attackText.text = "Attack: " + attack.ToString();
    }
}
