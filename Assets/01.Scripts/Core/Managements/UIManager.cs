using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public void Init()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
} 
