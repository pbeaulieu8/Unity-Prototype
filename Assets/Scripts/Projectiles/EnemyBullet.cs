using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{    
    Rigidbody2D rigidbody2d;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }    
        
    void OnCollisionEnter2D(Collision2D other) 
    {
        PlayerController p = other.collider.GetComponent<PlayerController>();
        //layer 8 is ground layer
        //layer 10 is enemy leater
        //layer 11 is player layer
        if(other.gameObject.layer == 8 || other.gameObject.layer == 11 || other.gameObject.layer == 10) {
            Destroy(gameObject);
        }
        if(p != null)
        {
            p.TakeDamage(2);
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
