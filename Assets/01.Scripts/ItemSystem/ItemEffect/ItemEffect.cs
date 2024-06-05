using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    protected Player _player;

    protected void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    public abstract void UseItem();
}