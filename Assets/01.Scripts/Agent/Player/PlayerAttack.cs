using Crogen.ObjectPooling;
using UnityEngine;

public class PlayerAttack : AgentAttack
{
    protected override void Awake()
    {
        base.Awake();
        _gameManager.InputReader.AttackEvent += HandleAttack;
    }

    private void OnDestroy()
    {
        _gameManager.InputReader.AttackEvent -= HandleAttack;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void HandleAttack()
    {
        OnAttack();
    }

    public override void OnAttack()
    {
        base.OnAttack();
        for (int i = 0; i < _BulletSpawnPoint.Length; ++i)
        {
            PlayerDefaultBullet bullet = this.Pop(
                        PoolType.PlayerDefualtBullet, 
                        _BulletSpawnPoint[i].position, 
                        _BulletSpawnPoint[i].rotation)
                        as PlayerDefaultBullet;
        }
    }
}