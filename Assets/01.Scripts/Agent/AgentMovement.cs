using System;
using DG.Tweening;
using UnityEngine;

public abstract class AgentMovement : MonoBehaviour
{
    //Values
    [field: SerializeField] public int DefaultSpeed { get; set; } = 50; 
    [field: SerializeField] public int MaxSpeed { get; set; } = 100;
    [field:SerializeField] public int CurSpeed { get; set; } = 0;
    [field:SerializeField] public float RotateSpeedX { get; set; } = 20f;
    [field:SerializeField] public float RotateSpeedY { get; set; } = 100f;
    
    private bool _isSpeedUp = false;
    private float _holdTime = 0f;
    
    //Actions
    public event Action OnSpeedDeadEvent; 
    
    #region Components
    protected Rigidbody _rbCompo { get; private set; }
    #endregion
    
    protected virtual void Awake()
    {
        CurSpeed = DefaultSpeed;
        _rbCompo = GetComponent<Rigidbody>();
    }
    protected virtual void FixedUpdate()
    {
        _rbCompo.velocity = transform.forward * CurSpeed;
    }
    protected virtual void Update()
    {
        OnSpeedChange(_isSpeedUp);
    }

    #region Speed Change
    
    private void OnSpeedChange(bool isSpeedUp)
    {
        if (isSpeedUp)
        {
            if (CurSpeed < MaxSpeed)
                _holdTime += Time.deltaTime;
            CurSpeed = (int)(MathExtension.PowerByTwo(_holdTime) * MaxSpeed) + DefaultSpeed;

            CurSpeed = Mathf.Clamp(CurSpeed, DefaultSpeed, MaxSpeed+DefaultSpeed);
        }
        else
        {
            _holdTime -= Time.deltaTime;
            CurSpeed = (int)(_holdTime * MaxSpeed) + DefaultSpeed;
            CurSpeed = Mathf.Clamp(CurSpeed, DefaultSpeed, MaxSpeed+DefaultSpeed);
            if (CurSpeed <= 0)
            {
                OnSpeedDeadEvent?.Invoke();
            }
        }
    }
    public void HandleSpeedUp()
    {
        _isSpeedUp = true;
    }
    public void HandleSpeedDown()
    {
        _isSpeedUp = false;
    }

    #endregion

    public abstract void HandleMoveDirection(Vector3 Delta);
}