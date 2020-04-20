using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementSpeed = 10.0f;
    private Rigidbody2D body;
    private Vector2 direction;
    private Vector2 lastShootDirection = Vector2.right;
    private Vector2 shootDirection = new Vector2();

    private bool mouseControl = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set Direction input
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

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
        
        // Move player
        body.velocity = direction.normalized * movementSpeed;
    }

    public Vector2 getShootDirection()
    {
        return shootDirection;
    }
}
