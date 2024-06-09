using UnityEngine;

public class M206FlareShooter : FlareShooter
{
    protected override void UpdateAttackCycles()
    {
        base.UpdateAttackCycles();
        for (int i = 0; i < 20; ++i)
        {
            ShootFlare(transform.forward - transform.up + (Random.onUnitSphere));
        }
    }
}