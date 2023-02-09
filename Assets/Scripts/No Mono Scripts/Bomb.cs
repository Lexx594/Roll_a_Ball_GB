using UnityEngine;
using Maze.Interface;

namespace Maze
{
    public sealed class Bomb : InteractiveObject, IFlay
    {
        private GameObject _player;
        private float _lengthFlay;

        [SerializeField] private AudioSource _audsActive;
        [SerializeField] private AudioSource _audsExplosion;

        private void Start()
        {
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
            if (!_player.GetComponent<Healthbar>()._freezeHealth)
            {
                _player.GetComponent<Healthbar>()._playerHealth -= 33.5f; 
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
