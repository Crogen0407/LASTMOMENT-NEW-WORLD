using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    //Managements
    private GameManager _gameManager;
    
    [SerializeField] private List<PoolType> _curWeapoPoolList;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }
    
}