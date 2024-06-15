using UnityEngine;

public class WeaponProductContent : MonoBehaviour
{
    [SerializeField] private WeaponElementContent[] _weaponElementContents;

    private void Awake()
    {
        //데이터 불러오는 거
        GameDataManager.Instance.LoadData();
        for (int i = 0; i < _weaponElementContents.Length; ++i)
        {
            _weaponElementContents[i].Init((WeaponEnum)i, (WeaponOwnState)GameDataManager.Instance.GameData.weaponOwnStateArray[i]);
        }
    }
}
