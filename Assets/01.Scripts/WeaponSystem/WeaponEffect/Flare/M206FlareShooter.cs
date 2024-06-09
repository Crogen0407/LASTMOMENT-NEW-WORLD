using UnityEngine;

public class M206FlareShooter : FlareShooter
{
    protected override void UpdateAttackCycles()
    {
        base.UpdateAttackCycles();
        for (int i = 0; i < 10; ++i)
        {
            ShootFlare(- MathExtension.VectorClamp(Random.onUnitSphere, new Vector3(-1, -1, 0), new Vector3(1, 1, 1)));
        }
    }
}