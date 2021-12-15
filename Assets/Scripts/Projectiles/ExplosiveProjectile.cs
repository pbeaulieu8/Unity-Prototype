using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{    
    Rigidbody2D rigidbody2d;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        //layer 10 is enemy layer
        if(other.gameObject.layer == 10) {
            EnemyController e = other.collider.GetComponent<EnemyController>();
            Destroy(gameObject);
            
            if(e != null)
            {
                e.TakeDamage(2);
            }
        }
        
        //layer 12 is destructible layer
        if(other.gameObject.layer == 12) {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
        
    }    
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
}
