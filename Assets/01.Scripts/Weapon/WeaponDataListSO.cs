using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/WeaponList")]
public class WeaponDataListSO : ScriptableObject
{
    public WeaponDataSO[] weapons;

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
