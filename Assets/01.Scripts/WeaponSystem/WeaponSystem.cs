using System.Collections.Generic;
using Crogen.ObjectPooling;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    //Managements
    private GameManager _gameManager;
    
    [SerializeField] private List<PoolType> _curWeapoPoolList;
    [SerializeField] private float[] _weaponCoolTimeMaxs;
    private float[] _curWeaponCoolTimes;

    [Header("UI")] 
    [SerializeField] private WeaponPanel[] _weaponPanels;
    
    private void Awake()
    {
        //Managements
        _gameManager = GameManager.Instance;
        
        //Values
        _curWeaponCoolTimes = _weaponCoolTimeMaxs;
        
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
        if(_weaponCoolTimeMaxs[value] <= _curWeaponCoolTimes[value])
            this.Pop(_curWeapoPoolList[value]);
    }

    private void Update()
    {
        for (int i = 0; i < _weaponPanels.Length; ++i)
        {
            bool isActive = _weaponCoolTimeMaxs[i] >= _curWeaponCoolTimes[i];
            if(isActive) continue;
            _curWeaponCoolTimes[i] += Time.deltaTime;
            _weaponPanels[i].UpdateOverloadPanelActive(!isActive);
        }
    }
}