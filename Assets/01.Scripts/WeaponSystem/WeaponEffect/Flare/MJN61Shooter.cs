using UnityEngine;

public class MJN61Shooter : FlareShooter
{
    protected override void UpdateAttackCycles()
    {
        base.UpdateAttackCycles();
        for (int i = 0; i < 2; ++i)
        {
            ShootFlare(transform.forward - transform.up + transform.right + (Random.onUnitSphere*0.5f));
            ShootFlare(transform.forward - transform.up + (Random.onUnitSphere*0.5f));
            ShootFlare(transform.forward - transform.up - transform.right + (Random.onUnitSphere*0.5f));
        }
    }
}