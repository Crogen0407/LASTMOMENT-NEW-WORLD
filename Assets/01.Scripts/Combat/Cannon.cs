using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _surpport;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Transform _attackPoint;
    
    [Header("Follow Target")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _targetFollowDelay = 5f;


    [Header("Flip")] 
    [SerializeField] private Vector3 _flipVec = Vector3.one;

    [Header("Clamp")] 
    [SerializeField] private float _minRotate = -180;
    [SerializeField] private float _maxRotate = 180;
    
    [Header("IsWorking")]
    public bool isWorking = true;
    
    void Update()
    {
        if (isWorking == false) return;
        Vector3 direction = _target.position - _attackPoint.position;

        for (int i = 0; i < 3; ++i)
            direction[i] *= _flipVec[i];

        float xRotate = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.z);
        float yRotate = Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z);
        xRotate = Mathf.Clamp(xRotate, _minRotate, _maxRotate);
        
        
        _surpport.DORotate(
            new Vector3(0, yRotate, 0), _targetFollowDelay);
        
        _muzzle.DOLocalRotate(
            new Vector3(xRotate, 0, 0), _targetFollowDelay);
    }
}
