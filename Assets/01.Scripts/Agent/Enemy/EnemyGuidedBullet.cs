using System.Collections;
using Crogen.ObjectPooling;
using DG.Tweening;
using UnityEngine;

public class EnemyGuidedBullet : MonoPoolingObject
{
    public float lifeTime = 2f;
    private float _currentLifeTime = 0;
    public float speed = 80f;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] protected PoolType _poolType;
    [SerializeField] protected PoolType _explosionEffect;
    [SerializeField] private float _findRadius = 200f;
    private Transform _findTargetTrm;
    private Vector3 _targetPos;

    private float _currentTargetFindTime = 0f;
    [SerializeField] private float _targetFindCoolTime = 1f;
    [SerializeField] private float _findRotateDelay = 1f;

    private Vector3 _moveDir;
    private bool _isChangingTargetPos = false;

    public override void OnPop()
    {
        TargetPointManager.Instance.ShowTargetPoint(transform, Color.red);

        _isChangingTargetPos = false;
        _currentLifeTime = 0;
        _currentTargetFindTime = 0;
        _moveDir = transform.forward.normalized;
        _findTargetTrm = GameManager.Instance.Player.transform;
        _targetPos = _findTargetTrm.position;
    }
    
    
    public override void OnPush()
    {
        TargetPointManager.Instance.CloseTargetPoint(transform);

        StopAllCoroutines();
        transform.DOKill();
        this.Pop(_explosionEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter()
    {
        DestroyImmdately();
    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;
        if(_currentLifeTime > lifeTime)
            Push(_poolType);

        _currentTargetFindTime += Time.deltaTime;
        //Å¸°Ù °»½Å
        if (_currentTargetFindTime > _targetFindCoolTime)
		{
            if (_isChangingTargetPos == false)
			{
                _targetPos = _findTargetTrm.position;
                StartCoroutine(CoroutineTargetPosSmoothChange());
            }
            _currentTargetFindTime = 0;
		}

        transform.position += _moveDir * speed * Time.deltaTime;
    }

    private IEnumerator CoroutineTargetPosSmoothChange()
	{
        _isChangingTargetPos = true;
        Vector3 currentDir = transform.forward.normalized;
        Vector3 endDir = (_targetPos - transform.position).normalized;
        float currentTime = 0;
        float duration = _findRotateDelay;
        while(duration > currentTime)
		{
            _moveDir = Vector3.Lerp(currentDir, endDir, currentTime / duration);
            transform.forward = _moveDir;
            currentTime += Time.deltaTime;
            yield return null;
		}
        yield return new WaitForSeconds(duration);
        _isChangingTargetPos = false;
    }

    public void DestroyImmdately()
	{
        Push(_poolType);
    }
}
