﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damage = 2;
    public float flightSpeed = 20.0f;
    public GameObject target;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void SetTarget(GameObject tgt)
    {
        target = tgt;
    }

    private void Move()
    {
        if (target != null)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, target.transform.position, flightSpeed * Time.deltaTime);
            rb2d.MovePosition(newPos);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            Destroy(gameObject);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == target)
        {
            EnemyController enemyTgt = target.GetComponent<EnemyController>();
            enemyTgt.Hit(damage);
            Destroy(gameObject);
        }
    }
}
