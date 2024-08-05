using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoSingleton<StageManager>
{
   [field:SerializeField] public float StageProgress { get; private set; }
   [SerializeField] private StageListDataSO _stageListData;
   [Header("Check Point")]
   [SerializeField] private List<Transform> _checkPointList;
   [SerializeField] private int _currentCheckPoint;
   public Transform currentTargetTrm;

   public bool gameClear = false;
   
   private void Awake()
   {
      InitCheckPoint();
   }

   private void InitCheckPoint()
   {
      _checkPointList[0].gameObject.SetActive(true);
      currentTargetTrm = _checkPointList[0];
      for (int i = 1; i < _checkPointList.Count; ++i)
         _checkPointList[i].gameObject.SetActive(false);
   }

   public void UpdateCurrentCheckPoint()
   {
      if (gameClear) return;
      ++_currentCheckPoint;
      if (_currentCheckPoint >= _checkPointList.Count)
      {
         //게임 클리어
         for (int i = 0; i < _checkPointList.Count; ++i)
         {
            _checkPointList[i].gameObject.SetActive(false);
         }
         gameClear = true;
         return;
      }

      for (int i = 0; i < _checkPointList.Count; ++i)
      {
         _checkPointList[i].gameObject.SetActive(_currentCheckPoint == i);
         if (_checkPointList[i].gameObject.activeSelf)
            currentTargetTrm = _checkPointList[i];
      }
   }
   
   public void GameClear()
   {
      TalkContent.Instance.OnTalk("System", "작전 성공", () =>
      {
         TalkContent.Instance.OnTalk("System", "ST-091, 본부로 귀환합니다", 1, null, () =>
         {
            int sceneIndex = _stageListData.list.FindIndex(x => x.stageName == SceneManager.GetActiveScene().name);
            GameDataManager.Instance.GameData.clearStageArray[sceneIndex] = 1;
            GameDataManager.Instance.SaveData();
            SceneLoadingManager.Instance.LoadingScene(SceneNames.LobbyScene, 5f);
         });
      });
   }
}