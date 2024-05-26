using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ACMDataList")]
public class ACMDataListSO : ScriptableObject
{
    public List<ACMDataSO> list;
}