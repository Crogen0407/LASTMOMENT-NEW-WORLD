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

    //Debug    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TalkContent.Instance.OnTalk("System", "누군가 말했다.");
        }
    }
}