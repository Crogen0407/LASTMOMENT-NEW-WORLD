using System;
using Crogen.PowerfulInput;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    private Vector3 _convertDir;
    [SerializeField] private InputReader _inputReader;
    
    public void OnEnable()
    {
        _inputReader.MovePlayerEvent += HandleMove;
        _inputReader.ChangeScrollEvent += HandleSpeedChange;
    }

    public void OnDisable()
    {
        _inputReader.MovePlayerEvent -= HandleMove;
        _inputReader.ChangeScrollEvent -= HandleSpeedChange;
    }
    
    private void HandleSpeedChange(float axis)
    {
        int speedValue = (int)(MaxSpeed * ((axis / 24) * 0.01f)); 
        
        CurSpeed += speedValue;

        CurSpeed = Mathf.Clamp(CurSpeed, 0, MaxSpeed);
    }
    
    protected override void HandleMove(Vector2 vec)
    {
        transform.forward *= vec;
    }

    private void FixedUpdate()
    {
        _rbCompo.velocity = transform.forward * CurSpeed;
    }
}
