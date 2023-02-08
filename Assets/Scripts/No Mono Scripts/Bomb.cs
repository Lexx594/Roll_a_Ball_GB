using UnityEngine;
using Maze.Interface;

namespace Maze
{
    public sealed class Bomb : InteractiveObject, IFlay
    {
        private GameObject _player;
        private float _lengthFlay;

        private void Start()
        {
            _player = GameObject.Find("Player").gameObject;
            _lengthFlay = Random.Range(0.5f, 1.0f);
        }
        protected override void Interaction()
        {
            var _healt = _player.GetComponent<Healthbar>();
            _healt._playerHealth -= 33.5f;
            Destroy(gameObject);
        }
        public void Flay()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
            0.5f + Mathf.PingPong(Time.time, _lengthFlay),
            transform.localPosition.z);
        }        
    }





}
