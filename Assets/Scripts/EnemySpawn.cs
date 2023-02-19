using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze
{

    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<GameObject> _enemyDeathPrefabs;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _maxEnemys;
        [SerializeField] private int _minEnemys;
        [SerializeField] private TextMeshProUGUI _leftEnemy;
        [SerializeField] private LayerMask _whatIsGround;
        public int enemyCount;
        public int leftKillEnemy;

        private void Awake()
        {
            _spawnPoints = new List<Transform>(_spawnPoints);
            _maxEnemys = DataHolder.maxEnemys;
            _minEnemys = DataHolder.minEnemys;
            enemyCount = UnityEngine.Random.Range(_minEnemys, _maxEnemys + 1);
            SpawnEnemy();
            if (enemyCount == 1)
            {
                _leftEnemy.text = "Для победы необходимо учичтожить одного противника";
            }
            else if (enemyCount == 2 || enemyCount == 3 || enemyCount == 4)
            {
                _leftEnemy.text = $"Для победы необходимо учичтожить {enemyCount} противника";
            }
            else if (enemyCount == 5 || enemyCount == 6 || enemyCount == 7 || enemyCount == 8 || enemyCount == 9 || enemyCount == 10)
            {
                _leftEnemy.text = $"Для победы необходимо учичтожить {enemyCount} противников";
            }
            leftKillEnemy = enemyCount;
        }

        private void Update()
        {
            if (leftKillEnemy != enemyCount) CheckKillEnemy();
            if (leftKillEnemy == 0) SceneManager.LoadScene(4);
        }

        public void CheckKillEnemy()
        {
            
        if (leftKillEnemy == 1)
            {
                 _leftEnemy.text = "Остался последний противник";
            }
            else if (leftKillEnemy == 2 || leftKillEnemy == 3 || leftKillEnemy == 4)
            {
                 _leftEnemy.text = $"Осталось {leftKillEnemy} противника";
            }
            else if (leftKillEnemy == 5 || leftKillEnemy == 6 || leftKillEnemy == 7 || leftKillEnemy == 8 || leftKillEnemy == 9 || leftKillEnemy == 10)
            {
                 _leftEnemy.text = $"Осталось {leftKillEnemy} противников";
            }
        }

        private void SpawnEnemy()
        {
            for (int i = 0; i < enemyCount; i++)
            {
                var spawn = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Instantiate(_enemyPrefab, _spawnPoints[spawn].transform.position, Quaternion.identity);
                _spawnPoints.RemoveAt(spawn);
            }
        }

        public void SpawnDeathEnemy(Vector3 position)
        {
            RaycastHit hit;
            Ray ray = new Ray(position, Vector3.down);

            if (Physics.Raycast(ray, out hit, 2f, _whatIsGround))
            {
                int i = UnityEngine.Random.Range(0, _enemyDeathPrefabs.Count);
                Instantiate(_enemyDeathPrefabs[i], hit.point, Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up) );
            }                
        }
    }
}
