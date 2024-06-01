using System;
using UnityEngine;

//스킬을 UI하고 연동해서 스킬 쿨타임을 보여주기 위해서
public delegate void CooldownInfoEvent(float current, float total);

public class Skill : MonoBehaviour
{
    public bool skillEnabled; //레벨 업해서 카드를 먹으면 활성화
    [SerializeField] protected bool _isPassiveSkill; //시간마다 발동되는 스킬
    [SerializeField] protected float _cooldown; //쿨타임

    [HideInInspector] public Player player;
    protected float _cooldownTimer;

    public event CooldownInfoEvent OnCooldownEvent;

    public LayerMask whatIsEnemy;

    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
    }

    protected virtual void Update()
    {
        if (_cooldown > 0)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
            }
            OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
        }
    }

    public virtual bool UseSkill()
    {
        if (_cooldownTimer > 0 || skillEnabled == false) return false;

        _cooldownTimer = _cooldown;
        return true;
    }

    public void UnlockSkill()
    {
        skillEnabled = true;
        if (_isPassiveSkill)
        {
            SkillManager.Instance.AddPassiveSkill(this); //나를 등록해주라 
        }
    }
}
