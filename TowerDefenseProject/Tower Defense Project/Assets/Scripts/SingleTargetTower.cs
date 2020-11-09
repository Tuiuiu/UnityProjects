using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetTower : TowerController
{
    GameObject currentTarget;
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
        ShootClosest();
        base.ShootBehaviour();
    }

    private void ShootClosest()
    {
        
        if (currentTarget != null && targets.Contains(currentTarget))
        {
            ShootTarget(currentTarget);
        }
        else
        {
            // Remove null items from targets
            targets.RemoveAll(item => item == null);
            FindAndAttackClosestTarget();
        }
    }

    private void FindAndAttackClosestTarget()
    {
        float distance = float.PositiveInfinity;
        GameObject newTgt = null;
        for (int i = 0; i < targets.Count; i++)
        {
            float testDist = Vector3.Distance(transform.position, targets[i].transform.position);
            if (testDist < distance)
            {
                distance = testDist;
                newTgt = targets[i];
            }
        }
        currentTarget = newTgt;
        if (newTgt != null) {
            ShootTarget(currentTarget);
        }
    }
}
