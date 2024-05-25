using System.Collections;
using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO _weaponData;
    
    public void UseWeapon()
    {
        StartCoroutine(CoroutineUseWeapon());
    }

    private IEnumerator CoroutineUseWeapon()
    {
        for (int i = 0; i < _weaponData.attackCount; ++i)
        {
            yield return new WaitForSeconds(_weaponData.attackDelayTime);
            for (int j = 0; j < _weaponData.effectCount; ++j)
            {
                MonoPoolingObject obj = this.Pop(
                    _weaponData.effectType, 
                    _weaponData.effectPosition[j], 
                    Quaternion.Euler(_weaponData.effectRotation[j]));
            }
        }
    }
}
