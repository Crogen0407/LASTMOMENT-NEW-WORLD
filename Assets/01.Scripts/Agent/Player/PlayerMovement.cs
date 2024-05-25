using UnityEngine;

public class PlayerMovement : AgentMovement
{
    //Managements
    private GameManager _gameManager;
    private UIManager _uiManager;

    public float aimingDistance;
    [HideInInspector] public Vector3 lookAngle = Vector3.zero;
    private bool _isChangeDirection;
    private Vector2 _lookDirectionAddValue;
    protected override void Awake()
    {
        base.Awake();
        _gameManager = GameManager.Instance;
        _uiManager = UIManager.Instance;

        _gameManager.InputReader.StartMoveDirectionEvent += StartMoveDirection;
        _gameManager.InputReader.EndMoveDirectionEvent += EndMoveDirection;
    }

    protected override void Update()
    {
        base.Update();
        if (_isChangeDirection)
        {
            HandleMoveDirection(_lookDirectionAddValue);
        }
    }

    private void StartMoveDirection(Vector3 vec)
    {
        _isChangeDirection = true;
        _lookDirectionAddValue = vec;
    }
    
    private void EndMoveDirection()
    {
        _isChangeDirection = false;
    }

    public override void HandleMoveDirection(Vector3 position)
    {
        Vector3 rotate = transform.rotation * new Vector3(
            -position.y * RotateSpeedY,
            position.x * RotateSpeedX,
            -position.x * 90f);
        transform.Rotate(rotate*Time.deltaTime, Space.World);
    }
}