using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon")]
public class WeaponSO : ScriptableObject
{
    //Values
    public int index;
    public PoolableObject effectObj;
    [Space(25)]
    public float delayTime = 20f;
    public float damage = 10f;
    public int attackCount = 1;
}
