using UnityEngine;

public interface IInteractable
{
    [field:SerializeField] public CheckPoint _checkPoint { get; set; }
    public void OnInteract();
}
