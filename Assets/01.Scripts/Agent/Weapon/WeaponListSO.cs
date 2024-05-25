using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponList")]
public class WeaponListSO : ScriptableObject
{
    public WeaponSO[] weapons;

    [ContextMenu("ResetIndex")]
    private void ResetIndex()
    {
        if (weapons != null)
        {
            for (int i = 0; i < weapons.Length; ++i)
            {
                if(weapons[i]!=null)
                    weapons[i].index = i;
            }
        }
    }
}
