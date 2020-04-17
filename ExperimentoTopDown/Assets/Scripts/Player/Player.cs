using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementSpeed = 10.0f;
    private Rigidbody2D body;
    private Vector2 direction;
    private Vector2 lastShootDirection = Vector2.right;
    public GameObject[] projectiles;

    private float autoRate = 1.0f;
    private float autoNext = 0f;

    private int shootDamage = 3;

    private bool mouseControl = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        autoNext = autoRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Set Direction input
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        Vector2 shootDirection = new Vector2();
        if (mouseControl)
        {
            Vector3 mapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 testDir = mapPoint - transform.position;
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

        // Shoot when fire button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(shootDirection);
        }

        // Upgrade fire rate of auto-weapon
        if (Input.GetButton("Jump"))
        {
            upgradeFireRate();
        }

        // Update auto-weapon cooldown time, and shoot if possible
        autoNext -= Time.deltaTime;
        if (autoNext <= 0f)
        {
            Shoot(shootDirection);
        }

        // Move player
        body.velocity = direction.normalized * movementSpeed;
    }

    private void Shoot(Vector2 shootDir)
    {
        Vector2 shootDirection = shootDir.normalized;
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);

        Projectile clone = Instantiate(projectiles[0], playerPosition + shootDirection, transform.rotation).GetComponent<Projectile>();
        // Projectile projectile = clone.GetComponent<Projectile>();
        clone.setDirection(shootDirection);
        clone.setDamage(shootDamage);
        autoNext = autoRate;
    }

    private void upgradeFireRate()
    {
        autoRate *= 0.9f;
    }
}
