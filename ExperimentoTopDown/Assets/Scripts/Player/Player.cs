﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player stats
    private int life;
    private bool invincible = false;
    private float invincibilityTime = 0f;
    // References
    private Rigidbody2D body;
    public LevelControllers level;
    // Movement
    private float movementSpeed = 10.0f;
    private Vector2 direction;
    // Aiming
    private Vector2 lastShootDirection = Vector2.right;
    private Vector2 shootDirection = new Vector2();
    private bool mouseControl = true;

    // Store variables
    private bool closeToStore = false;


    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        body = GetComponent<Rigidbody2D>();
        level = GameObject.Find("LevelControllers").GetComponent<LevelControllers>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Direction input
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        if (!LevelControllers.GameIsPaused)
        {
            if (mouseControl)
            {
                Vector3 aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 testDir = aimPoint - transform.position;
                shootDirection.x = testDir.x;
                shootDirection.y = testDir.y;
            }

            else
            {
                // Check shooting direction. If it's zero, change to last valid direction
                shootDirection = direction;
                if (shootDirection.magnitude < 0.02)
                {
                    shootDirection = lastShootDirection;
                }
                else
                {
                    lastShootDirection = shootDirection;
                }
            }

            if (Input.GetButtonDown("Interact") && closeToStore)
            {
                level.OpenWorkshop();
            }
        }



        // Invincibility timer
        if (invincible)
        {
            invincibilityTime -= Time.deltaTime;
            if (invincibilityTime <= 0)
            {
                invincible = false;
            }
        }

        Vector2 perpendicular = Vector2.Perpendicular(shootDirection);
        transform.right = -perpendicular;

        // Move player
        body.velocity = direction.normalized * movementSpeed;
    }

    public Vector2 getShootDirection()
    {
        return shootDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!invincible)
            {
                TakeDamage();
                invincible = true;
                invincibilityTime = 3.0f;
            }
        }

        else if (collision.CompareTag("Nanobot")) 
        {
            level.AddNanobot();
        }

        else if (collision.CompareTag("ShopInteract"))
        {
            closeToStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ShopInteract"))
        {
            closeToStore = false;
        }
    }

    private void TakeDamage()
    {
        life -= 1;
        Debug.Log("VIDA: " + life);
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("MORREU");      
    }
}
