using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum SettingOption
{
    //Game
    XSensitivity,
    YSensitivity,
    
    //Sound
    
    
    //Graphic
}

[CreateAssetMenu(menuName = "SO/SettingOptionData")]
public class SettingOptionDataSO : ScriptableObject
{
    public SerializedDictionary<SettingOption, string> uiDescriptionDictionary;
}