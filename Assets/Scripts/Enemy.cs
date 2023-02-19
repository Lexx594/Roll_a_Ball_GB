using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Maze
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private AudioSource _audsStep;


        private GameObject _player;
        private Rigidbody _rb;
        public LayerMask whatIsGround, whatIsPlayer;

        [SerializeField] private float _sightRange, _attackRange;
        public bool _playerInSightRange, _playerInAttackRange;


        public Vector3 walkPoint;
        private bool _walkPointSet;
        [SerializeField] private float _walkPointRange; 




        private bool _isDeath = false;
        private bool _isWait = true;
        private bool _step;



        void Awake()
        {
            _player = GameObject.Find("Player").gameObject;
            _agent = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            var direction = _player.transform.position - transform.position;
            RaycastHit hit;
            Ray ray = new Ray(transform.position, direction);

            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, whatIsPlayer);
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, whatIsPlayer);

            if (!_playerInSightRange && !_playerInAttackRange && !_isDeath) Patroling();

            if (_playerInSightRange && !_playerInAttackRange)
            {
                if (Physics.Raycast(ray, out hit, _sightRange))
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.tag == _player.tag && !_player.GetComponent<Healthbar>()._freezeHealth
                            && !_player.GetComponent<PlayerBall>()._isDisembodied) ChasePlayer();
                        else Patroling();
                    }
                }                             
                
            }
            //if (_playerInSightRange && _playerInAttackRange && !_isDeath) AttackPlayer();

            if (_agent.velocity.magnitude != 0f)_step = true;
            else _step = false;
            Step();


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


        private void Patroling()
        {
            //GetComponent<NavMeshAgent>().speed = 3f;
            //если точка патрулирования не задана создаем новую точку патрулирования
            if (!_walkPointSet && _isWait) SearchWalkPoint();
            //если точка патрулирования задана, то агент устанавливает точку патрулирования
            if (_walkPointSet) _agent.SetDestination(walkPoint);
            // расчитываем растояние до точки патрулирования
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            //если растояние меньше 1, то мы пришли
            if (distanceToWalkPoint.magnitude < 2f)
            {
                // точка патрулирования не установлена
                _walkPointSet = false;
                if (_isWait)
                {                    
                    _isWait = false;
                    Invoke(nameof(Wait), Random.Range(3f, 30f));
                }
            }
        }

        private void Wait()
        {
            _isWait = true;
        }

        private void SearchWalkPoint() //создаем случайную точку обхода
        {            
            float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);
                        
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            {                
                _walkPointSet = true;
            }
        }

        private void ChasePlayer()
        {
            _walkPointSet = true;                       
            _agent.SetDestination(_player.transform.position);
            if (_isDeath) GetComponent<NavMeshAgent>().speed = 0f;      
        }






        public void EnemyDestroy() { Destroy(gameObject); }
    }







}
