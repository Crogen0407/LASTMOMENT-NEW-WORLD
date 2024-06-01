using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum SettingOptionType
{
    //Game
    XSensitivity,
    YSensitivity,
    
    //Sound
    MasterVolume,
    BGM,
    SFX,
    
    //Graphic
    ImageQuality,
    FPS,
    WindowMode
}

[CreateAssetMenu(menuName = "SO/SettingOptionData")]
public class SettingOptionDataSO : ScriptableObject
{
    public SerializedDictionary<SettingOptionType, string> uiDescriptionDictionary;
}