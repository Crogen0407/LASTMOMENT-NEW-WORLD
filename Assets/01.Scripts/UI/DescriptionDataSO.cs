using System;
using UnityEngine;

[Serializable]
public struct DescriptionData
{
    public string title;
    public string description;
    public Sprite image;
}

[CreateAssetMenu(menuName = "SO/Description/DescriptionData")]
public class DescriptionDataSO : ScriptableObject
{
    public DescriptionData[] descriptionDatas;
}