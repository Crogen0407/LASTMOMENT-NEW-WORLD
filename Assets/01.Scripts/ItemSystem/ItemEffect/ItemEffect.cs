using System;
using UnityEngine;

[Serializable]
public abstract class ItemEffect : MonoBehaviour
{
    public abstract void UseItem();
}