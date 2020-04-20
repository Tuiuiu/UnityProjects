using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    private Player playerComponent;

    private float autoRate = 1.0f;
    private float autoNext = 0f;

    private int shootDamage = 3;

    // Start is called before the first frame update
    void Start()
    {
        autoNext = autoRate;
        playerComponent = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
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
        autoNext -= Time.deltaTime;
        if (autoNext <= 0f)
        {
            Shoot(ShootDirection());
        }
    }

    private void Shoot(Vector2 shootDir)
    {
        Vector2 shootDirection = shootDir.normalized;
        Vector2 weaponPosition = new Vector2(transform.position.x, transform.position.y);

        Projectile clone = Instantiate(projectile, weaponPosition + shootDirection, transform.rotation).GetComponent<Projectile>();
        // Projectile projectile = clone.GetComponent<Projectile>();
        clone.setDirection(shootDirection);
        clone.setDamage(shootDamage);
        autoNext = autoRate;
    }

    private void upgradeFireRate()
    {
        autoRate *= 0.9f;
    }

    private Vector2 ShootDirection()
    {
        return playerComponent.getShootDirection();
    }
}
