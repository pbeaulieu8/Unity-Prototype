using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorUpgradeCostText : MonoBehaviour
{
    private Text costText;
    
    private int cost;

    // Start is called before the first frame update
    void Start()
    {
        costText = GetComponent<Text>();
        cost = 100;
        costText.text = cost.ToString();
    }

    public void UpdateCost(int newCost) {
        cost = newCost;
        costText.text = cost.ToString();
    }
}
