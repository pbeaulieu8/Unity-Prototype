using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHP;

    void OnTriggerEnter2D(Collider2D other) {
        boss.SetActive(true);
        bossHP.SetActive(true);
    }
}
