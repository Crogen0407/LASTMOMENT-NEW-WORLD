using Crogen.PowerfulInput;
using UnityEngine;

public class PlayerMovement : AgentMovement
{
    private Vector3 _convertDir;
    [SerializeField] private InputReader _inputReader;

    public void OnEnable()
    {
        _inputReader.MovePlayerEvent += Move;
    }
    
    public void OnDisable()
    {
        _inputReader.MovePlayerEvent -= Move;
    }
    
    protected override void Move(Vector2 mousePos)
    {
        Vector3 cameraPos = Camera.main.ScreenToWorldPoint(mousePos);
        cameraPos = new Vector3(0, cameraPos.y, cameraPos.z);
        
        _convertDir = (cameraPos).normalized;
        Debug.Log(_convertDir);
        float dis = Vector3.Distance(cameraPos, transform.position);
        if (dis > 0.3f)
        {
            _rbCompo.velocity = _convertDir * Speed;
        }
    }
}
