using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class ActiveBomb : MonoBehaviour
    {
        private float _radius = 3f;
        private float _force = 2000f;
        private GameObject _player;
        private GameObject _enemySpawn;

        [SerializeField] private AudioSource _audsActive;
        [SerializeField] private AudioSource _audsExplosion;


        private void Start()
        {
            try
            {
                _player = GameObject.Find("Player").gameObject;
            }
            catch(NullReferenceException)
            {
                Debug.Log("NullReferenceException");
            }
            try
            {
                _enemySpawn = GameObject.Find("====ENEMYS====").gameObject;
            }
            catch (NullReferenceException)
            {
                Debug.Log("NullReferenceException");
            }
                        
            Invoke(nameof(Explosion), 3f);
            _audsActive.Play();
        }

        void Explosion()
        {
            _audsActive.Stop();
            _audsExplosion.Play();
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);


            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                Rigidbody rb = overlappedColliders[i].attachedRigidbody;
                if (rb != null)
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);

                    Bomb staticBomb = rb.GetComponent<Bomb>();
                    if (staticBomb != null) { staticBomb.BombDestroy(); }

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
                        enemy.EnemyDestroy();
                    }
                }

            }

            Invoke(nameof(BombDestroy), 2f);
        }


        public void BombDestroy() { Destroy(gameObject); }


    }
}
