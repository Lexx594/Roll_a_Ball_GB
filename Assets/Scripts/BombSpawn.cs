using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{

    public class BombSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _bomb;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _maxBombs;
        [SerializeField] private int _minBombs;


        private void Awake()
        {
            _spawnPoints = new List<Transform>(_spawnPoints);
            SpawnBonus();
        }

        private void SpawnBonus()
        {
            for (int i = 0; i < UnityEngine.Random.Range(_maxBombs, _minBombs); i++)
            {
                var spawn = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Instantiate(_bomb, _spawnPoints[spawn].transform.position, Quaternion.identity);
                _spawnPoints.RemoveAt(spawn);
            }
        }
    }
}
