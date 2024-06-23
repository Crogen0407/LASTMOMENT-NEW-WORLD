using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum AudioType
{
    //BGM
    BGM_Cyber,
    BGM_Empty,
    BGM_Horror,
    BGM_LostSoul,
   
    //SFX
    SFX_AcquiringItems,
    SFX_BombExplosion,
    SFX_BulletHit,
    SFX_ChargeLaser,
    SFX_EnemyExplosion,
    SFX_FireBullet,
    SFX_ExploisonNoise,
    SFX_LaserCharge,
    SFX_PlayerDie,
    SFX_SpaceShipMove,
    SFX_UIClick,
    SFX_BossExplosion
    
}

[CreateAssetMenu( menuName = "SO/SoundSystem/SoundData")]

public class SoundDataSO : ScriptableObject
{
    public SerializedDictionary<AudioType, AudioClip> audioClipDictionary;
}
