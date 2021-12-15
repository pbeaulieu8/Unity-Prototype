using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : EnemyController
{
    Rigidbody2D rigidbody2d;
    public GameObject bulletPrefab;

    public GameObject target;
    Rigidbody2D targetRigidbody;

    public float shotTimerMax;
    private float shotTimer;

    float dist;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetRigidbody = target.gameObject.GetComponent<Rigidbody2D>();
        shotTimer = 0;
    }

    void Update() {
        shotTimer -= Time.deltaTime;
        
        dist = Vector3.Distance(target.transform.position, transform.position);

        if(shotTimer <= 0) {
            GameObject bulletObject = Instantiate(bulletPrefab, rigidbody2d.position+Vector2.down*0.8f, Quaternion.identity);
            
            EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>();

            bullet.Launch((targetRigidbody.position-rigidbody2d.position).normalized, 300);
            shotTimer = shotTimerMax;
        }
    }
}
