using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{

    public class BonusSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject[] _bonus;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private int _maxBonus;
        [SerializeField] private int _minBonus;


        private void Awake()
        {
            _spawnPoints = new List<Transform>(_spawnPoints);
            SpawnBonus();
        }

        private void SpawnBonus()
        {
            for (int i = 0; i < UnityEngine.Random.Range(_minBonus, _maxBonus); i++) 
            {
                AddNewBonus();
            }
        }

        public void AddNewBonus()
        {
            var spawn = UnityEngine.Random.Range(0, _spawnPoints.Count);
            Instantiate(_bonus[UnityEngine.Random.Range(0, _bonus.Length)], _spawnPoints[spawn].transform.position, Quaternion.identity);
            //_spawnPoints.RemoveAt(spawn);
            //Debug.Log(spawn);
        }
    }
}
