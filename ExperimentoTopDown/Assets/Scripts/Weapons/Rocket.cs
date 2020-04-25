using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // AQUI PRECISA EXPLODIR TUTU
    protected override void OnHitConfirmed(Enemy enemy)
    {
        // Hit Damage
        base.OnHitConfirmed(enemy);

        // Check for enemies hit by explosion
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        
        // Explosion deals secondary effects to each enemy affected by it, including explosion damage
        foreach (Collider2D enm in enemiesHit)
        {
            if (enm.gameObject.CompareTag("Enemy"))
            {
                Enemy collateralTarget = enm.GetComponent<Enemy>();
                foreach (ShotEffect eff in weapon.secondaryEffects)
                    collateralTarget.resolveEffect(eff);
            }
        }
        // Destroy rocket object
        Destroy(gameObject);
    }
}
