using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : EnemyController
{
    private Transform target;

    public float speed;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        target = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);
        if(dist <= 9f) {
            transform.position = Vector2.MoveTowards(transform.position,target.position, speed * Time.deltaTime);
        }
    }
}
