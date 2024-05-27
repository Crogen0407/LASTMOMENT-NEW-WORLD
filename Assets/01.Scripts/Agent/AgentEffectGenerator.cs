using UnityEngine;

public class AgentEffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _vfxHighBusters;
    
    public void SetHighBusterEffect(bool active)
    {
        for (int i = 0; i < _vfxHighBusters.Length; ++i)
        {
            _vfxHighBusters[i].SetActive(active);
        }
    }
}
