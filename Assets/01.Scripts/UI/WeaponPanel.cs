using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attackCount;
    [SerializeField] private GameObject _overloadPanel;

    
    public void UpdateAttackCount(int value)
    {
        _attackCount.text = value == 0 ? "-" : value.ToString();
    }

    public void UpdateOverloadPanelActive(bool active)
    {
        _overloadPanel.SetActive(active);
    }
}
