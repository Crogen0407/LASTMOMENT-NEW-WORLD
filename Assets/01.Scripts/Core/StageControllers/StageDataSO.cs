using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct StageMention
{
    public string[] mentions;
    public UnityEvent mentionEvent;
} 

[CreateAssetMenu(menuName = "SO/StageDataSO")]
public class StageDataSO : ScriptableObject
{
    public List<StageMention> stageMentionList;
}
