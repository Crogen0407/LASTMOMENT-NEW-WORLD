using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private PoolType _poolType;

    public abstract void UseWeapon();
}
