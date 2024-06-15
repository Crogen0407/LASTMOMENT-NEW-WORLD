using UnityEngine;

public class WeaponProductContent : MonoBehaviour
{
    public WeaponElementContent[] weaponElementContents;
    
    private void Awake()
    {
        //데이터 불러오는 거
        GameDataManager.Instance.LoadData();
        for (int i = 0; i < weaponElementContents.Length; ++i)
        {
            weaponElementContents[i].Init((WeaponEnum)(i+1), (WeaponOwnState)GameDataManager.Instance.GameData.weaponOwnStateArray[i]);
        }
    }
}