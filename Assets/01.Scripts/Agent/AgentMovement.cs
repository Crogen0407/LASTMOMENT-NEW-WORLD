using System;
using UnityEngine;

public abstract class AgentMovement : MonoBehaviour
{
    //Values
    [field: SerializeField] public int DefaultSpeed { get; set; } = 50; 
    [field: SerializeField] public int MaxSpeed { get; set; } = 100;
    [field:SerializeField] public int CurSpeed { get; set; } = 0;
    public Renderer[] busterVFXMaterials;
    private int _busterVFXShaderID;
    private bool _isSpeedUp = false;
    private float _holdTime = 0f;
    
    //Actions
    public event Action OnStopEvent; 
    
    #region Components
    protected Rigidbody _rbCompo { get; private set; }
    #endregion
    
    protected virtual void Awake()
    {
        _rbCompo = GetComponent<Rigidbody>();
        CurSpeed = DefaultSpeed;
        _busterVFXShaderID = Shader.PropertyToID("_Scale");
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

            CurSpeed = Mathf.Clamp(CurSpeed, DefaultSpeed, MaxSpeed);
            if (Mathf.Approximately(CurSpeed, MaxSpeed))
            {
                foreach (var t in busterVFXMaterials)
                {
                    t.material.SetFloat(_busterVFXShaderID, 2f);
                }
            }
        }
        else
        {
            _holdTime -= Time.deltaTime;
            CurSpeed = (int)(_holdTime * MaxSpeed) + DefaultSpeed;
            CurSpeed = Mathf.Clamp(CurSpeed, DefaultSpeed, MaxSpeed);
            if (Mathf.Approximately(CurSpeed, DefaultSpeed))
            {
                OnStopEvent?.Invoke();
                foreach (var t in busterVFXMaterials)
                {
                    t.material.SetFloat(_busterVFXShaderID, 1f);
                }
            }
        }
    }
    public virtual void HandleSpeedChange(bool value)
    {
        _isSpeedUp = value;
    }

    #endregion

    public abstract void HandleMoveDirection(Vector3 Delta);
}