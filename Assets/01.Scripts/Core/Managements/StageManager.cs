using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
   [field:SerializeField] public float StageProgress { get; private set; }
   [SerializeField] private StageDataSO _stageData;

   private List<Enemy> _enemies;
   private int _maxEnemyCount;
   private void Awake()
   {
      _enemies = FindObjectsOfType<Enemy>().ToList();
      _maxEnemyCount = _enemies.Count;
   }

   public void DeCountEnemy(Enemy enemy)
   {
      for (int i = 0; i < _enemies.Count; ++i)
      {
         if (_enemies[i] == enemy)
         {
            _enemies.Remove(enemy);
            StageProgress = ((float)(_maxEnemyCount - _enemies.Count) / _maxEnemyCount);
            break;
         }
      }
   }
}