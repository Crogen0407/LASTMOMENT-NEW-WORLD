using Crogen.ObjectPooling;
using UnityEngine;

public class FloatingBombEffect : ItemEffect
{
    public override void UseItem()
    {
        Vector3 vec = _player.transform.position;

        this.Pop(PoolType.FloatingBomb, vec, Quaternion.identity);
    }
}