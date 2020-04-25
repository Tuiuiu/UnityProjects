using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        fireDelay = 3.0f;
        effects.Add(new ShotEffect(EffectType.PhysicalDamage, 5, -1));
        secondaryEffects.Add(new ShotEffect(EffectType.ExplosionDamage, 3, -1));
        secondaryEffects.Add(new ShotEffect(EffectType.Burn, 1, 4));
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
