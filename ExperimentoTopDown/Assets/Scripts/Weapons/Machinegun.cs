﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        fireDelay = 1.0f;
        shootDamage = 3;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
