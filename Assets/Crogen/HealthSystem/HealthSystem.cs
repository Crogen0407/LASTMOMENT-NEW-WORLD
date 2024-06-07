using UnityEngine;

namespace Crogen.HealthSystem
{
    public abstract class HealthSystem : MonoBehaviour
    {
        [Header("Hp Option")]
        [SerializeField] private float _hp = 100.0f;
        public float maxHp = 100.0f;
        public bool isImpassible;
        [HideInInspector] public bool isDie = false;
        
        protected virtual void Awake()
        {
            _hp = maxHp;
        }

        public float Hp
        {
            get => _hp;
            set
            {
                if (isDie) return;
                if (gameObject.activeSelf == true)
                {
                    if(_hp < value)
                    {
                        OnHpUp();
                    }
                    else if (_hp > value)
                    {
                        OnHpDown();
                    }

                    if (!isImpassible || _hp <= value)
                    {
                        _hp = value;
                    }
                    _hp = Mathf.Clamp(_hp, 0, maxHp);
                    
                    if (_hp <= 0.1f)
                    {
                        isDie = true;
                        OnDie();
                    }                
                }
                OnHpChange();
            }
        }

        protected abstract void OnHpChange();
        protected abstract void OnHpUp();
        protected abstract void OnHpDown();
        protected abstract void OnDie();
    }    
}