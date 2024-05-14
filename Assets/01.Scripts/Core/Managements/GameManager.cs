using Crogen.PowerfulInput;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public InputReader InputReader { get; private set; }
    
    //Managements
    public UIManager UIManager { get; private set; }

    private void Awake()
    {
        UIManager = UIManager.Instance;

        InputReader.MouseClickEvent += UIManager.Init;
    }
}