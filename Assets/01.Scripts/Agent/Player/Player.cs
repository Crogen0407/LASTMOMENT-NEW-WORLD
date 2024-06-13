using Cinemachine;
using Crogen.AgentFSM;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : Agent<AgentStateEnum>
{
    public PlayerAttack PlayerAttack { get; private set; }
    [SerializeField] private CinemachineVirtualCamera _playerDieVirtualCamera;

    private Transform _visualTrm;
    
    [Header("PowerUp")]
    [SerializeField] private GameObject _powerUpSkinnedEffect;
    
    private bool _powerUp;
    public bool PowerUp
    {
        get=>_powerUp;
        set
        {
            PlayerAttack.ChangeBulletEffect(value);
            _powerUpSkinnedEffect.SetActive(value); 
            _powerUp = value;
        }
    }
    
    [Header("Barrier")] 
    [SerializeField] private GameObject _barrierEffect;
    
    private bool _barrier;
    public bool Barrier
    {
        get => _barrier;
        set
        {
            _barrier = value;
            _barrierEffect.SetActive(_barrier);
            HealthSystem.isImpassible = _barrier;
        }
    }

    [Header("Stamina")]
    private float _stamina = 20;
    public float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = value;
            _stamina = Mathf.Clamp(_stamina, 0, maxStamina);
            _staminaSlider.value = _stamina/maxStamina;

            if (_stamina < maxStamina * 0.5f)
            {
                _staminaSlider.colors = new ColorBlock()
                {
                    normalColor = new Color(0.9f, 0.75f, 0.2f, 1),
                    colorMultiplier = 1
                };
            }
            else
            {
                _staminaSlider.colors = new ColorBlock()
                {
                    normalColor = Color.white,
                    colorMultiplier = 1
                };
            }
        }
    }
    public float maxStamina = 20;
    [SerializeField] private Slider _staminaSlider;

    protected override void Awake()
    {
        base.Awake();
        _visualTrm = transform.Find("Visual");
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    public override void SetDead()
    {
        base.SetDead();
        _visualTrm.gameObject.SetActive(false);
        GameManager.Instance.GameOver();
    }
}