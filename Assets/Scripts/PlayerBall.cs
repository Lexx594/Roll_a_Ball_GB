using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maze
{
    public class PlayerBall : Player
    {
        public bool _isDisembodied;

        void Update()
        {
            Move();

            if (_isDisembodied)
            {
                _rb.useGravity = false;
                GetComponent<SphereCollider>().isTrigger = true;
            }
            else
            {
                _rb.useGravity = true;
                GetComponent<SphereCollider>().isTrigger = false;
            }

        }
    }
}
