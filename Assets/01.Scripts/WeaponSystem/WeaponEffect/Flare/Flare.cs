using Crogen.HealthSystem;
using Crogen.ObjectPooling;
using UnityEngine;

public class Flare : MonoPoolingObject
{
    [SerializeField] private PoolType _flarePoolType;
    [SerializeField] private PoolType _explosionPoolType;
    [HideInInspector] public float damage;

    public override void OnPop()
    {
    }

    public override void OnPush()
    {
        this.Pop(_explosionPoolType,transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Hp -= damage;
        }
        else if (other.transform.transform.parent.TryGetComponent(out HealthSystem healthSystemInParent))
        {
            healthSystemInParent.Hp -= damage;
        }
        else if (other.transform.TryGetComponent(out EnemyGuidedBullet enemyGuidedBullet))
		{
            enemyGuidedBullet.DestroyImmdately();
        }
        Push(_flarePoolType);
    }
}