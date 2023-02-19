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
            catch (NullReferenceException)
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
                    var direction = rb.transform.position - (transform.position + Vector3.up * 2.2f);
                    RaycastHit hit;
                    Ray ray = new Ray(transform.position + Vector3.up * 2.2f, direction);
                    
                    if (Physics.Raycast(ray, out hit, _radius))
                    {
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject == rb.gameObject)
                            {
                                rb.AddExplosionForce(_force, transform.position, _radius);

                                Bomb staticBomb = rb.GetComponent<Bomb>();
                                if (staticBomb != null) { staticBomb.BombDestroy(); }
                                //staticBomb? staticBomb.BombDestroy(); 

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
                }
            }
            Invoke(nameof(BombDestroy), 2f);
        }


        public void BombDestroy() { Destroy(gameObject); }
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position + Vector3.up * 3, _player.transform.position);
        }
    }
}
