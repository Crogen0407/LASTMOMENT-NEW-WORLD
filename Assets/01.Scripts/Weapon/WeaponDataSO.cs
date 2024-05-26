using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    //Values
    public WeaponEnum WeaponEnum;
    
    [Header("Effect")]
    public int effectCount;
    public Vector3[] effectPosition;
    public Vector3[] effectRotation;
    public PoolType effectType;
    
    [Header("Value")]
    public int attackCount = 1;
    public float attackDelayTime = 20f;
    public float attackDamage = 10f;
}
