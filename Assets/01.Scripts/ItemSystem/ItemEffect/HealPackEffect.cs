using Crogen.ObjectPooling;
using UnityEngine;

public class HealPackEffect : ItemEffect
{
    [SerializeField] private int _healthValue = 20;
    
    public override void UseItem()
    {
        _player.HealthSystem.Hp += _healthValue;
    }
}