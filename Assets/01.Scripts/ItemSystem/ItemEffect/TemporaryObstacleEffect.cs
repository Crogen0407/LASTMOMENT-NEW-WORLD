using Crogen.ObjectPooling;
using UnityEngine;

public class TemporaryObstacleEffect : ItemEffect
{
    public override void UseItem()
    {
        Vector3 vec = _player.transform.position;

        this.Pop(PoolType.TemporaryObstacle, vec, Quaternion.identity);
    }
}
