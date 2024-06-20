using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoSingleton<SoundManager>
{
   [SerializeField] private AudioSource _sfxAudioSourcePrefab;

   [Header("BGM")] 
   public AudioType bgmAudioType;
   
   private AudioSource _bgmAudioSource;
   
   [SerializeField] private List<AudioSource> _audioSourceList;

   [Header("Clips")] 
   [SerializeField] private SoundDataSO _soundData;

   private void Awake()
   {
      Button[] allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
      for (int i = 0; i < allButtons.Length; i++)
      {
         allButtons[i].onClick.AddListener(PlayUISFX);
      }
   }

   public void PlayBGM(bool isFade = false, float beforeDelay = 0f, float fadeDuration = 1f, float eneValue = 1f)
   {
      _bgmAudioSource ??= GetComponent<AudioSource>();
      
      _bgmAudioSource.clip = _soundData.audioClipDictionary[bgmAudioType];
      _bgmAudioSource.loop = true;
      
      if (isFade)
      {
         _bgmAudioSource.volume = 1-eneValue;
         _bgmAudioSource.Play();
         Sequence seq = DOTween.Sequence();
         seq.AppendInterval(beforeDelay);
         seq.Append(_bgmAudioSource.DOFade(eneValue, fadeDuration).SetUpdate(true).SetEase(Ease.OutExpo));
      }
      else
      {
         _bgmAudioSource.volume = 1;
         _bgmAudioSource.Play();
      }
   }
   
   public void PlayUISFX()
   {
      _audioSourceList ??= new List<AudioSource>(); 

      var audioObj = _audioSourceList.Find(x => x.gameObject.activeSelf == false);
      if (audioObj == null)
      {
         audioObj = Instantiate(_sfxAudioSourcePrefab, transform);
         _audioSourceList.Add(audioObj);
      }
      else
         audioObj.gameObject.SetActive(true);
      
      audioObj.spatialBlend = 0f;
      audioObj.clip = _soundData.audioClipDictionary[AudioType.SFX_UIClick];
      StartCoroutine(AddElementList(audioObj, false));
   }
   

   public void PlaySFX(AudioType audioType, Vector3 position, bool isFade = false, float fadeDuration=1f, bool isBlend = false, float canHearSoundRange = 100f)
   {
      _audioSourceList ??= new List<AudioSource>();

      var audioObj = _audioSourceList.Find(x => x.gameObject.activeSelf == false);
      if (audioObj == null)
      {
         audioObj = Instantiate(_sfxAudioSourcePrefab, transform);
         _audioSourceList.Add(audioObj);
      }
      else
         audioObj.gameObject.SetActive(true);
      
      //서라운드 효과      
      if (isBlend)
      {
         audioObj.spatialBlend = 1f;
         audioObj.maxDistance = canHearSoundRange;
         audioObj.transform.position = position;
      }
      else
      {
         audioObj.spatialBlend = 0f;
      }
      
      audioObj.clip = _soundData.audioClipDictionary[audioType];
      StartCoroutine(AddElementList(audioObj, isFade, fadeDuration));
   }

   private IEnumerator AddElementList(AudioSource audioSource, bool isFade, float fadeDuration = 1f)
   {
      if (isFade)
      {
         audioSource.volume = 0;
         audioSource.Play();
         Sequence seq = DOTween.Sequence();
         seq.Append(_bgmAudioSource.DOFade(1, fadeDuration).SetUpdate(true).SetEase(Ease.OutExpo));
      }
      else
      {
         audioSource.volume = 1;
         audioSource.Play();
      }

      yield return new WaitForSeconds(audioSource.clip.length);
      audioSource.Stop();
      audioSource.gameObject.SetActive(false);
   }
   
   private void OnValidate()
   {
      _bgmAudioSource = GetComponent<AudioSource>();
      _bgmAudioSource.loop = true;
      _bgmAudioSource.playOnAwake = false;
   }
}