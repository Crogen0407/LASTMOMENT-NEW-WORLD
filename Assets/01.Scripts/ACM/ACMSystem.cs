using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ACMSystem : MonoBehaviour
{
    [SerializeField] private ACMDataListSO acmDataList;

    public void UseTechnique(ACMEnum enumType)
    {
        ACMDataSO currentAcmData=acmDataList.list.Find(x => x.enumType == enumType);
        
        for (int i = 0; i < currentAcmData.techniqueTransforms.Length; ++i)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMove(currentAcmData.techniqueTransforms[i].position,
                currentAcmData.techniqueTransforms[i].duration));
            seq.Join(transform.DORotate(currentAcmData.techniqueTransforms[i].rotation,
                currentAcmData.techniqueTransforms[i].duration));
            seq.Join(transform.DOScale(currentAcmData.techniqueTransforms[i].scale,
                currentAcmData.techniqueTransforms[i].duration));
            seq.AppendInterval(currentAcmData.techniqueTransforms[i].delay);
        }
    }
}
