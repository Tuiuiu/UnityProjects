﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
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

    protected override void OnHitConfirmed(Enemy enemy)
    {
        Debug.Log("Bullet hit an enemy!!");
        enemy.takeDamage(damage);
        Destroy(gameObject);
    }
}