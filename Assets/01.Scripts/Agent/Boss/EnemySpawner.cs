using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    
    public void SpawnEnemy(float spawnDelay, float duration, UnityAction endEvent)
    {
        StartCoroutine(CoroutineEnemySpawn(spawnDelay, duration, endEvent));
    }

    private IEnumerator CoroutineEnemySpawn(float spawnDelay, float duration, UnityAction endEvent)
    {
        float currentTime = 0;
        float delayTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            delayTime += Time.deltaTime;
            if (delayTime > spawnDelay)
            {
                int enemyRange = Random.Range(0, _enemyPrefabs.Length);
                int spawnPointRange = Random.Range(0, _spawnPoints.Length);
                Instantiate(_enemyPrefabs[enemyRange], _spawnPoints[spawnPointRange].position, Quaternion.LookRotation(_spawnPoints[spawnPointRange].forward));
                delayTime = 0;
            }
            yield return null;
        }
        endEvent?.Invoke();
    }
}