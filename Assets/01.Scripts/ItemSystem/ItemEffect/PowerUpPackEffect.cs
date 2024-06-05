using System.Collections;
using UnityEngine;

public class PowerUpPackEffect : ItemEffect
{
    [SerializeField] private float _duration = 10f; 
    private bool _isPowerUp;
    
    public override void UseItem()
    {
        StopAllCoroutines();
        StartCoroutine(CoroutinePowerUp());
    }

    private IEnumerator CoroutinePowerUp()
    {
        _player.PowerUp = true;
        yield return new WaitForSeconds(_duration);
        _player.PowerUp = false;
    }
}
