using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 4f;
        public Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        protected void Move()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            _rb.AddForce(movement * moveSpeed);
            
        }
    }
}
