using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{    
    Rigidbody2D rigidbody2d;
    int attack;

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
        //layer 10 is enemy layer
        if(other.gameObject.layer == 10) {
            EnemyController e = other.gameObject.GetComponent<EnemyController>();

            if(e != null)
            {
                Destroy(gameObject);
                e.TakeDamage(attack);
            }   
        }
        //layer 13 is boss layer
        else if(other.gameObject.layer == 13) {
            BossController b = other.gameObject.GetComponent<BossController>();

            if(b != null)
            {
                Destroy(gameObject);
                b.TakeDamage(attack);
            }
        }
        //layer 11 is player layer
        else if(other.gameObject.layer != 11) {
            Destroy(gameObject);
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
    
    public void Launch(Vector2 direction, float force, int attackValue)
    {
        attack = attackValue;
        rigidbody2d.AddForce(direction * force);
    }
}
