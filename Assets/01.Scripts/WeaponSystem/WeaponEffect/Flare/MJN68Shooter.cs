using UnityEngine;

public class MJN68Shooter : FlareShooter
{
    protected override void UpdateAttackCycles()
    {
        base.UpdateAttackCycles();
        for (int i = 0; i < 3; ++i)
        {
            ShootFlare(transform.forward + transform.right + (Random.onUnitSphere*0.1f));
            ShootFlare(transform.forward - transform.up + (Random.onUnitSphere*0.1f));
            ShootFlare(transform.forward - transform.right + (Random.onUnitSphere*0.1f));
        }
    }
}
