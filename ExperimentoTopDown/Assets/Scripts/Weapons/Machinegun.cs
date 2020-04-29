using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Weapon
{
    // Set weapon properties, then call base Start
    protected override void Start()
    {
        fireDelay = 1.0f;
        effects.Add(new ShotEffect(EffectType.PhysicalDamage, 3, -1));
        effects.Add(new ShotEffect(EffectType.Stun, -1, 2));
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
