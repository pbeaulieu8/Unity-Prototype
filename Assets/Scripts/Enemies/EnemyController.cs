using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{   
    public int HP;
    public int damage;
    public int money;
    public GameObject player;

    // Start is called before the first frame update
    public void Start()
    {
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if(HP <= 0) {
            Destroy(gameObject);
            player.gameObject.GetComponent<PlayerController>().AddMoney(money);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if(playerController != null) {
            playerController.TakeDamage(damage);
        }
    }
}
