using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
   [field:SerializeField] public float StageProgress { get; private set; }
   [SerializeField] private StageDataSO _stageData;
   
   [Header("Check Point")]
   [SerializeField] private List<Transform> _checkPointList;
   [SerializeField] private int _currentCheckPoint;

   private bool _gameClear = false;
   
   private void Awake()
   {
      InitCheckPoint();
   }

   private void InitCheckPoint()
   {
      _checkPointList[0].gameObject.SetActive(true);
      for (int i = 1; i < _checkPointList.Count; ++i)
         _checkPointList[i].gameObject.SetActive(false);
   }

   public void UpdateCurrentCheckPoint()
   {
      if (_gameClear) return;
      ++_currentCheckPoint;
      if (_currentCheckPoint >= _checkPointList.Count)
      {
         //게임 클리어
         GameManager.Instance.GameClear();
         _gameClear = true;
         return;
      }
      for (int i = 0; i < _checkPointList.Count; ++i)
         _checkPointList[i].gameObject.SetActive(_currentCheckPoint == i);
   }
}