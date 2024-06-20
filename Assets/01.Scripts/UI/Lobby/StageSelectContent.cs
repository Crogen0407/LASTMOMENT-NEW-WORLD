using DG.Tweening;
using UnityEngine;

public class StageSelectContent : MonoBehaviour
{
    private RectTransform _rectTransform;

    [Header("Movement")]
    [SerializeField] private float _minPos;
    [SerializeField] private float _maxPos;

    [SerializeField] private RectTransform _scrollContent;
    [SerializeField] private StageElement _stageElementPrefab;
    [SerializeField] private StageListDataSO _stageListData;
    
    private void Awake()
    {
        //데이터 없다면 만들고
        GameDataManager.Instance.LoadData();
        int[] clearStageArray = GameDataManager.Instance.GameData.clearStageArray;
        if (clearStageArray.Length != _stageListData.list.Count)
        {
            GameDataManager.Instance.GameData.clearStageArray = new int[_stageListData.list.Count];
            clearStageArray = GameDataManager.Instance.GameData.clearStageArray;
            GameDataManager.Instance.SaveData();
        }
        
        //데이터 읽기
        int maxClearStageIndex = 0;
        for (int i = 0; i < clearStageArray.Length; ++i)
        {
            if (clearStageArray[i] == 0)
            {
                maxClearStageIndex = i+1;
                if (maxClearStageIndex > _stageListData.list.Count)
                    maxClearStageIndex = _stageListData.list.Count;
                break;
            }
        }
        
        _rectTransform = transform as RectTransform;
        for (int i = 0; i < maxClearStageIndex; ++i)
        {
            int index = i;
            StageElement stageElement = Instantiate(_stageElementPrefab, _scrollContent);
            stageElement.SetText(_stageListData.list[index].uiStageName);
            stageElement.SetMissionClear(i<maxClearStageIndex-1);
            stageElement.AddListener(() =>
            {
                SceneLoadingManager.Instance.LoadingScene(_stageListData.list[index].stageName);
            });
        }
    }

    public void SetActiveStageSelectContent()
    {
        float endPos = 0f;
        endPos = Mathf.Approximately(_rectTransform.anchoredPosition.x, _minPos) ? _maxPos : _minPos;
        _rectTransform.DOAnchorPosX(endPos, 0.5f);
        GameDataManager.Instance.SaveData();
    }
}
