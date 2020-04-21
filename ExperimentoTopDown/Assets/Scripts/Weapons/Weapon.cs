using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Player reference
    private GameObject player;
    private Player playerComponent;

    // Type of projectile
    public GameObject projectile;

    // Shoot properties
    private float nextShot = 0f;
    protected float fireDelay = 1.0f;
    protected int shootDamage = 3;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        nextShot = fireDelay;
        player = transform.parent.gameObject;
        playerComponent = player.GetComponent<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Shoot when fire button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(ShootDirection());
        }

        // Upgrade fire rate of auto-weapon
        if (Input.GetButton("Jump"))
        {
            upgradeFireRate();
        }

        // Update auto-weapon cooldown time, and shoot if possible
        nextShot -= Time.deltaTime;

        if (nextShot <= 0f)
        {
            Shoot(ShootDirection());
        }
    }

    private void Shoot(Vector2 shootDir)
    {
        Vector2 shootDirection = shootDir.normalized;
        Vector2 weaponPosition = new Vector2(transform.position.x, transform.position.y);

        Projectile clone = Instantiate(projectile, weaponPosition + shootDirection, transform.rotation).GetComponent<Projectile>();
        clone.setDirection(shootDirection);
        clone.setDamage(shootDamage);
        nextShot = fireDelay;
    }

    // Increse fire rate by decreasing the delay between shots
    private void upgradeFireRate()
    {
        fireDelay *= 0.9f;
    }

    // Get player's aim direction
    private Vector2 ShootDirection()
    {
        return playerComponent.getShootDirection();
    }
}
