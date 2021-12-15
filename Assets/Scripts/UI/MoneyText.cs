using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour
{
    private Text moneyText;

    int money;

    void Start() {
        money = 0;
        moneyText = GetComponent<Text>();
        
        moneyText.text = money.ToString();
    }

    public void AddMoney(int moneyIncrease) {
        money += moneyIncrease;
        moneyText.text = money.ToString();
    }
}
