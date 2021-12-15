using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpgrade : MonoBehaviour
{
    public GameObject playerObject;
    private PlayerController player;

    int upgradeCost;
    int upgradeLevel;

    public AttackUpgradeCostText upgradeCostText;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<PlayerController>();
        upgradeCost = 100;
        upgradeLevel = 0;
    }

    // Update is called once per frame
    public void Upgrade()
    {
        int money = player.GetMoney();

        if(money >= upgradeCost) {
            money -= upgradeCost;
            
            player.SetAttack(player.GetAttack() + 1);
            player.AddMoney(-upgradeCost);
                        
            upgradeLevel += 1;
            upgradeCost += 100;

            upgradeCostText.UpdateCost(upgradeCost);
        } 
    }

    public int GetCost() {
        return upgradeCost;
    }

}
