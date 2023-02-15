using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Maze
{
    public class PlayerBall : Player
    {
        public bool _isDisembodied;
        private bool _step;
        private bool _nearEnemy;
        private Vector3 _oldPosition;
        [SerializeField] private AudioSource _audsStep;
        [SerializeField] private AudioSource _audsNearEnemy;
        [SerializeField] private LayerMask _whatIsEnemy;


        private void Start()
        {
            _oldPosition = transform.position;
        }


        void Update()
        {
            Move();
            Step();
            NearEnemy();

            if (_isDisembodied)
            {
                _rb.useGravity = false;
                GetComponent<SphereCollider>().isTrigger = true;
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                _rb.useGravity = true;
                GetComponent<SphereCollider>().isTrigger = false;
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (transform.position != _oldPosition)_step = true;
            else _step = false;

            _nearEnemy = Physics.CheckSphere(transform.position, 10f, _whatIsEnemy);



        }

        void FixedUpdate()
        {
            _oldPosition = transform.position;
        }



        void NearEnemy()
        {
            bool f = true;
            if (!_nearEnemy && f)
            {
                _audsNearEnemy.Play();
                f = false;
            }
            else if (_nearEnemy && !f)
            {
                _audsNearEnemy.Stop();
                f = true;
            }
        }


        void Step()
        {
            bool f = true;
            if (!_step && f)
            {
                _audsStep.Play();
                f = false;
            }
            else if (_step && !f)
            {
                _audsStep.Stop();
                f = true;
            }
        }
    }
}
