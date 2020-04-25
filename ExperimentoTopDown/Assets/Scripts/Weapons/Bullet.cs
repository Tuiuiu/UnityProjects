using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    // Start is called before the first frame update
    protected override void Start()
    {
        speed = 40.0f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnHitConfirmed(Enemy enemy)
    {
        base.OnHitConfirmed(enemy);
        Destroy(gameObject);
    }
}
