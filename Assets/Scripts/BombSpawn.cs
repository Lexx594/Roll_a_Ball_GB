using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{

    public class BombSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _maxBombs;
        [SerializeField] private int _minBombs;


        private void Awake()
        {
            _spawnPoints = new List<Transform>(_spawnPoints);
            SpawnBombs();
        }

        private void SpawnBombs()
        {
            for (int i = 0; i < UnityEngine.Random.Range(_minBombs, _maxBombs+1); i++)
            {
                var spawn = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Instantiate(_bombPrefab, _spawnPoints[spawn].transform.position, Quaternion.identity);
                _spawnPoints.RemoveAt(spawn);
            }
        }
    }
}
