using System;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    //Values
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;
    
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
    public void HandleSpeedUp()
    {
        _isSpeedUp = true;
    }
    public void HandleSpeedDown()
    {
        _isSpeedUp = false;
    }
    public void HandleMoveDirection(Vector2 mouseDelta)
    {
        lookAngle += new Vector3(
            -mouseDelta.y * RotateSpeedY, 
            mouseDelta.x * RotateSpeedX * 0.5f, 
            0) * Time.deltaTime * ((float)CurSpeed/MaxSpeed*0.5f);
        lookAngle = new Vector3(MathExtension.RotateClamp(lookAngle.x, -90f, 90f), lookAngle.y, lookAngle.z);
        transform.eulerAngles = lookAngle;
    }
    private void OnSpeedChange(bool isSpeedUp)
    {
        if (isSpeedUp)
        {
            if (CurSpeed < MaxSpeed)
                _holdTime += Time.deltaTime;
            CurSpeed = (int)(EaseInCubic(_holdTime) * MaxSpeed);

            CurSpeed = Mathf.Clamp(CurSpeed, 0, MaxSpeed);
        }
        else
        {
            _holdTime -= Time.deltaTime;
            CurSpeed = (int)(EaseInDefault(_holdTime) * MaxSpeed);
            CurSpeed = Mathf.Clamp(CurSpeed, 0, MaxSpeed);
            if (CurSpeed <= 0)
            {
                OnSpeedDeadEvent?.Invoke();
            }
        }
    }
    private float EaseInCubic(float x) 
    {
        return x * x * x;
    }
    private float EaseInDefault(float x) 
    {
        return x;
    }
}
