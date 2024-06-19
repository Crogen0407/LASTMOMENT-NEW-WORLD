using Crogen.JsamJson;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponManager : MonoBehaviour
{
    //Managements
    private GameManager _gameManager;
    [SerializeField] private WeaponDataSO _weaponData;
    
    [SerializeField] private WeaponEnum[] _curWeapons;
    [SerializeField] private float[] _weaponCoolTimeMaxs;
    private float[] _curWeaponCoolTimes;
    [SerializeField] private int[] _weaponAttackCounts;

    [Header("UI")] 
    [SerializeField] private WeaponPanel[] _weaponPanels;
    [FormerlySerializedAs("_productData")] [SerializeField] private WeaponProductDataSO weaponProductData;

    private Transform _playerTrm;
    
    private void LoadCurWeaponData() 
    {
        GameDataManager.Instance.LoadData();
        for (int i = 0; i < GameDataManager.Instance.CurrentWeaponArray.Length; ++i)
        {
            _curWeapons[i] = (WeaponEnum)GameDataManager.Instance.CurrentWeaponArray[i];    
        }
        
    }
    
    private void Awake()
    {
        //빌드할 떄 풀기
        LoadCurWeaponData();
        
        //Managements
        _gameManager = GameManager.Instance;
        
        //Values
        _weaponCoolTimeMaxs = new float[_curWeapons.Length];
        _curWeaponCoolTimes = new float[_curWeapons.Length];
        _weaponAttackCounts = new int[_curWeapons.Length];
        
        for (int i = 0; i < _curWeapons.Length; ++i)
        {
            _weaponCoolTimeMaxs[i] = _weaponData.weaponDataDictionary[_curWeapons[i]].coolTime;
            _curWeaponCoolTimes[i] = _weaponCoolTimeMaxs[i];
            _weaponAttackCounts[i] = _weaponData.weaponDataDictionary[_curWeapons[i]].attackCount;
        }
        
        //UI
        for (int i = 0; i < _weaponPanels.Length; ++i)
        {
            _weaponPanels[i].Init(weaponProductData.weaponProductDataDictionary[_curWeapons[i]]);
            _weaponPanels[i].SetAttackCount(_weaponAttackCounts[i]);
        }

        _playerTrm = GameManager.Instance.Player.transform;
        
        //Events
        _gameManager.InputReader.UseWeaponEvent += HandleUseWeapon;
    }

    private void OnDestroy()
    {
        //Events
        _gameManager.InputReader.UseWeaponEvent -= HandleUseWeapon;
    }

    private void HandleUseWeapon(int value)
    {
        if (_curWeapons[value] == WeaponEnum.None || 
            _curWeaponCoolTimes[value] < _weaponCoolTimeMaxs[value]) return;
        _weaponPanels[value].SetAttackCount(--_weaponAttackCounts[value]);
        
        //여기서 불릿 만들고
        WeaponEffect weaponEffect = Instantiate(
            _weaponData.weaponDataDictionary[_curWeapons[value]].weaponEffectPrefab,
            _playerTrm.position, 
            Quaternion.identity).GetComponent<WeaponEffect>();
        
        weaponEffect.Init(_playerTrm.forward, _playerTrm);
        _curWeaponCoolTimes[value] = 0;
    }
    
    private void Update()
    {
        for (int i = 0; i < _weaponPanels.Length; ++i)
        {
            bool isActive = _weaponCoolTimeMaxs[i] <= _curWeaponCoolTimes[i];
            if (isActive)
            {
                _weaponPanels[i].SetOverloadPanelActive(false);
                continue;
            }
            _curWeaponCoolTimes[i] += Time.deltaTime;
            _weaponPanels[i].SetOverloadPanelActive(true);
        }
    }
}