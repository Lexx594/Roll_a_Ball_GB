using UnityEngine;
using Maze.Interface;

namespace Maze
{
    public sealed class Bomb : InteractiveObject, IFlay
    {
        private float _radius = 3f;
        private float _force = 2000f;
        private GameObject _player;
        private GameObject _enemySpawn;
        private float _lengthFlay;

        [SerializeField] private AudioSource _audsActive;
        [SerializeField] private AudioSource _audsExplosion;

        private void Start()
        {
            _enemySpawn = GameObject.Find("====ENEMYS====").gameObject;
            _player = GameObject.Find("Player").gameObject;
            _lengthFlay = Random.Range(0.5f, 1.0f);
        }
        protected override void Interaction()
        {
            //if(!_player.GetComponent<Healthbar>()._freezeHealth && !_player.GetComponent<PlayerBall>()._isDisembodied)
            //{
            //    Invoke(nameof(Damage), 1.5f);
            //    _audsActive.Play();

            //}

            if (!_player.GetComponent<PlayerBall>()._isDisembodied)
            {
                Invoke(nameof(Damage), 1.5f);
                _audsActive.Play();
                transform.GetChild(1).gameObject.SetActive(true);                
            }
        }

        private void Damage()
        {
            _audsExplosion.Play();
            Invoke(nameof(BombDestroy), 2f);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            //if (!_player.GetComponent<Healthbar>()._freezeHealth)
            //{
            //    _player.GetComponent<Healthbar>()._playerHealth -= 33.5f;
            //    _player.GetComponent<Healthbar>().FreezeHealthOnAvtoOff();
            //}

            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                Rigidbody rb = overlappedColliders[i].attachedRigidbody;
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                                      

                    Healthbar playerDamage = rb.GetComponent<Healthbar>();
                    if (playerDamage != null && !playerDamage._freezeHealth)
                    {
                        playerDamage._playerHealth -= 33.5f;
                        playerDamage.FreezeHealthOnAvtoOff();
                    }

                    Enemy enemy = rb.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        _enemySpawn.GetComponent<EnemySpawn>().leftKillEnemy -= 1;
                        _enemySpawn.GetComponent<EnemySpawn>().SpawnDeathEnemy(enemy.transform.position);
                        enemy.EnemyDestroy();
                    }
                }
            }



        }



        public void Flay()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
            0.5f + Mathf.PingPong(Time.time, _lengthFlay),
            transform.localPosition.z);
        }        

        public void BombDestroy() { Destroy(gameObject); }
    }





}
