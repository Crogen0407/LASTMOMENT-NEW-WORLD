using Crogen.PowerfulInput;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public InputReader InputSystem { get; private set; }
}