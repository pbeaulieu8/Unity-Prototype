using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemyController : EnemyController
{
    public float speed;
    public float changeTime = 3.0f;

    float timer;
    int direction = 1;

    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {        
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {        
        Vector2 position = rigidbody2D.position;
        position.x = position.x + Time.deltaTime * speed * direction;

        rigidbody2D.MovePosition(position);
    }
}
