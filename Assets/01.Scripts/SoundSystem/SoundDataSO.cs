using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum AudioType
{
    //BGM
    BGM_Empty,
    BGM_Horror,
   
    //SFX
    SFX_ExploisonNoise,
    SFX_UIClick,
}

[CreateAssetMenu( menuName = "SO/SoundSystem/SoundData")]
public class SoundDataSO : ScriptableObject
{
    public SerializedDictionary<AudioType, AudioClip> audioClipDictionary;
}
