using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;
        public Rigidbody _rb;
        //public bool _step;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            moveSpeed = DataHolder.moveSpeed;
        }

        protected void Move()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            _rb.AddForce(movement * moveSpeed);
            //if (movement != Vector3.zero) _step = true;
            //else _step = false;

        }
    }
}
