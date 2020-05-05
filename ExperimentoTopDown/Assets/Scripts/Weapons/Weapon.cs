using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum of different types of effects that a shot can have
public enum EffectType
{
    ExplosionDamage,
    PhysicalDamage,
    Freeze,
    Burn,
    Stun
}

// Defines all the stats of a possible effect caused  
// by a projectile present on a weapon
public struct ShotEffect
{
    public EffectType type;
    public int value;
    public float duration;

    // Struct constructor
    public ShotEffect(EffectType eff, int val, float dur)
    {
        type = eff;
        value = val;
        duration = dur;
    }
}


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
    public List<ShotEffect> effects = new List<ShotEffect>();
    public List<ShotEffect> secondaryEffects = new List<ShotEffect>();


    // Start is called before the first frame update
    // Set autofire and get Player reference
    protected virtual void Start()
    {
        nextShot = fireDelay;
        player = transform.parent.gameObject;
        playerComponent = player.GetComponent<Player>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!LevelControllers.GameIsPaused)
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
    }

    //  Instantiate a projectile, directed to aim direction, carrying weapon damage and effects properties via reference
    private void Shoot(Vector2 shootDir)
    {
        Vector2 shootDirection = shootDir.normalized;
        Vector2 weaponPosition = new Vector2(transform.position.x, transform.position.y);
        Projectile clone = Instantiate(projectile, weaponPosition + shootDirection, transform.rotation).GetComponent<Projectile>();
        clone.setDirection(shootDirection);
        clone.setWeapon(this);
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
