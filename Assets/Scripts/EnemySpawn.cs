using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze
{

    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _EnemyPrefab;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _maxEnemys;
        [SerializeField] private int _minEnemys;
        [SerializeField] private TextMeshProUGUI _leftEnemy;
        private int _enemyCount;
        public int leftKillEnemy;

        private void Awake()
        {
            _spawnPoints = new List<Transform>(_spawnPoints);
            _maxEnemys = DataHolder.maxEnemys;
            _minEnemys = DataHolder.minEnemys;
            _enemyCount = UnityEngine.Random.Range(_minEnemys, _maxEnemys + 1);
            SpawnEnemy();
            if (_enemyCount == 1)
            {
                _leftEnemy.text = "Для победы необходимо учичтожить одного противника";
            }
            else if (_enemyCount == 2 || _enemyCount == 3 || _enemyCount == 4)
            {
                _leftEnemy.text = $"Для победы необходимо учичтожить {_enemyCount} противника";
            }
            else if (_enemyCount == 5 || _enemyCount == 6 || _enemyCount == 7 || _enemyCount == 8 || _enemyCount == 9 || _enemyCount == 10)
            {
                _leftEnemy.text = $"Для победы необходимо учичтожить {_enemyCount} противников";
            }
            leftKillEnemy = _enemyCount;
        }

        private void Update()
        {
            if(leftKillEnemy < _enemyCount)
            {
                if (leftKillEnemy == 1)
                {
                    _leftEnemy.text = "Остался последний противник";
                }
                else if (leftKillEnemy == 2 || leftKillEnemy == 3 || leftKillEnemy == 4)
                {
                    _leftEnemy.text = $"Осталось {leftKillEnemy} противника";
                }
                else if (leftKillEnemy == 5 || leftKillEnemy == 6 || leftKillEnemy == 7 || leftKillEnemy == 8 || leftKillEnemy == 9 )
                {
                    _leftEnemy.text = $"Осталось {leftKillEnemy} противников";
                }
            }
            if (leftKillEnemy == 0)
            {
                SceneManager.LoadScene(4);
            }

        }

        private void SpawnEnemy()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                var spawn = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Instantiate(_EnemyPrefab, _spawnPoints[spawn].transform.position, Quaternion.identity);
                _spawnPoints.RemoveAt(spawn);
            }
        }
    }
}
