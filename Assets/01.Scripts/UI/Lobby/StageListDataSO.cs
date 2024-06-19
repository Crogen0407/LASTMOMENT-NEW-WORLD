using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StageNames
{
    public string stageName;
    public string uiStageName;
}

[CreateAssetMenu(menuName = "SO/UI/StageListData")]
public class StageListDataSO : ScriptableObject
{
    public List<StageNames> list;
}
