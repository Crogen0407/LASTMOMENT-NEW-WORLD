using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum SettingOption
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
    public SerializedDictionary<SettingOption, string> uiDescriptionDictionary;
}