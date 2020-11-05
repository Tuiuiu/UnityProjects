using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetTower : TowerController
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

    protected override void ShootBehaviour()
    {
        ShootAll();
        base.ShootBehaviour();
    }

    private void ShootAll()
    {
        for (int i = targets.Count - 1; i >= 0; i--)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
            }
            else
            {
                ShootTarget(targets[i]);
            }
        }
    }
}
