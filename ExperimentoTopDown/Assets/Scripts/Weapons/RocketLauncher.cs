﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        fireDelay = 3.0f;
        shootDamage = 5;
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}