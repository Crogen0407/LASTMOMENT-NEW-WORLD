using UnityEngine;

public abstract class BossPattern : MonoBehaviour
{
    private BossAttack _bossAttack;
    
    public void Init(BossAttack bossAttack)
    {
        _bossAttack = bossAttack;
    }
    
    public virtual void EnterPattern(){}

    public virtual void ExitPattern()
    {
        ++_bossAttack.CurrentPatternIndex;
    }
}