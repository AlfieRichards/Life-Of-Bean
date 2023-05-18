using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _theEnemy;
    [SerializeField] private GameObject _aiArea;
    [SerializeField] private float _timeBetweenSpawn;
    float _timer;

    public int _maxEnemies;
    public int _enemyCount = 0;

    void Awake() 
    {
        _timer = _timeBetweenSpawn;    
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            SpawnEnemy();
            _timer = _timeBetweenSpawn;
        }
    }

    void SpawnEnemy()
    {
        if(_enemyCount >= _maxEnemies) return;
        GameObject enemy = Instantiate(_theEnemy, this.transform.position, this.transform.rotation);
        enemy.transform.SetParent(_aiArea.transform);
        _enemyCount++;
    }
}
