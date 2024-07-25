using Crogen.AgentFSM;
using DG.Tweening;
using System;
using UnityEngine;

public class Boss : Agent<BossStateEnum>
{
    //Managements
    private StageManager _stageManager;
    private UIManager _uiManager;  
    
    public LayerMask whatIsPlayer;
    public float recognitionRange = 50f;
    
    public EnemyType enemyType;

    public BossAttack bossAttack;
    public EnemySpawner enemySpawner;

    public Transform visualTrm;

    protected override void Awake()
    {
        base.Awake();
        bossAttack = GetComponent<BossAttack>();
        enemySpawner = GetComponent<EnemySpawner>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, recognitionRange);
    }
    
    public override void SetDead()
    {
        base.SetDead();

        CameraManager.Instance.FadePlayerCameraShack(10, 300, 200);
        visualTrm.DOShakePosition(100, Vector3.zero * 2f);
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            SoundManager.Instance.PlaySFX(AudioType.SFX_ExploisonNoise, transform.position);
        });
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            SoundManager.Instance.PlaySFX(AudioType.SFX_BossExplosion, transform.position);
            Destroy(gameObject);
        });
    }
}