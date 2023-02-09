using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        private void Start()
        {
            _player = GameObject.Find("Player").gameObject;
        }
        private void OnTriggerEnter(Collider other)
        {       
            if (other.tag == "Player") _player.GetComponent<PlayerBall>()._isDisembodied = false;                           
        }
        private void OnTriggerExit(Collider other)
        {            
            if (other.tag == "Player") _player.GetComponent<PlayerBall>()._isDisembodied = true;
        }
    }
}
