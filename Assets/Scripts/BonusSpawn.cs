using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            _maxBonus = DataHolder.maxBonus;
            _minBonus = DataHolder.minBonus;            
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
            Collider[] overlappedColliders = Physics.OverlapSphere(_spawnPoints[spawn].transform.position, 1);
            //Debug.Log(overlappedColliders.Length);

            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                if (overlappedColliders[i].gameObject.GetComponent<GreenBonus>() != null ||
                    overlappedColliders[i].gameObject.GetComponent<RedBonus>() != null ||
                    overlappedColliders[i].gameObject.GetComponent<Bomb>() != null)
                {
                    AddNewBonus();
                    break;
                }
                if (i == overlappedColliders.Length -1)
                {
                    Instantiate(_bonus[UnityEngine.Random.Range(0, _bonus.Length)], _spawnPoints[spawn].transform.position + Vector3.up * 0.5f, Quaternion.identity);
                }
            }          
        }
    }
}
