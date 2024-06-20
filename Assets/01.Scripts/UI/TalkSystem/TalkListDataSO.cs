using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TalkData
{
    public string owner;
    public string talk;
    public float textDelay = 0.1f;
    public float talkDelay = 1f;
}

[CreateAssetMenu( menuName = "SO/TalkSystem/TalkListData")]
public class TalkListDataSO : ScriptableObject
{
    public List<TalkData> list;
}
