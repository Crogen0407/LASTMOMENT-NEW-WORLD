using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
   [field:SerializeField] public float StageProgress { get; private set; }
   [SerializeField] private StageDataSO _stageData;
}
