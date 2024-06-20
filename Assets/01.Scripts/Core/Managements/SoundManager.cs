using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public enum AudioType
{
   //BGM
   
   //SFX
   
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoSingleton<SoundManager>
{
   [SerializeField] private AudioSource _sfxAudioSourcePrefab;
   private AudioSource _bgmAudioSource;

   private List<AudioSource> _audioSourceList;
   
   [Header("Clips")]
   [SerializeField] private SerializedDictionary<AudioType, AudioClip> _audioClipDictionary;

   public void PlayBGM(AudioType audioType)
   {
      _bgmAudioSource ??= GetComponent<AudioSource>();
      
      _bgmAudioSource.clip = _audioClipDictionary[audioType];
      _bgmAudioSource.loop = true;
      _bgmAudioSource.Play();
   }

   public void PlaySFX(AudioType audioType, Vector3 position, float canHearSoundRange = 100f)
   {
      _audioSourceList ??= new List<AudioSource>();
      
      var audioObj = Instantiate(_sfxAudioSourcePrefab, transform);
      audioObj.spatialBlend = 1f;
      audioObj.maxDistance = canHearSoundRange;
      audioObj.transform.position = position;
      audioObj.clip = _audioClipDictionary[audioType];
      audioObj.Play();
      _audioSourceList.Add(audioObj);
   }

   private void OnValidate()
   {
      _bgmAudioSource = GetComponent<AudioSource>();
      _bgmAudioSource.loop = true;
      _bgmAudioSource.playOnAwake = false;
   }
}