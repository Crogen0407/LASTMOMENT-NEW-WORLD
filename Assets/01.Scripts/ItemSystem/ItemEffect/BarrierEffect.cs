using System.Collections;
using UnityEngine;

public class BarrierEffect : ItemEffect
{
    [SerializeField] private float _duration = 3f; 
    public override void UseItem()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineBarrier());
    }

    private IEnumerator CoroutineBarrier()
    {
        _player.Barrier = true;
        yield return new WaitForSeconds(_duration);
        _player.Barrier = false;
    }
}
