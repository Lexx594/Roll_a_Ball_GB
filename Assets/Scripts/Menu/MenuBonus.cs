using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maze.Interface;
using Maze;

namespace MazeMenu
{
    public class MenuBonus : MonoBehaviour, IFlay, IRotator
    {
        private float _lengthFlay;

        void Start()
        {
            _lengthFlay = Random.Range(0.5f, 1.0f);


        }

        void Update()
        {
            Flay();
            Rotation();
        }

        public void Flay()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
            0.5f + Mathf.PingPong(Time.time*0.5f, _lengthFlay),
            transform.localPosition.z);
        }
        public void Rotation()
        {
            transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        }

    }
}
