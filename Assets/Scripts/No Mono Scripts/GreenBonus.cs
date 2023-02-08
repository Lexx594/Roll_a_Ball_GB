using UnityEngine;
using Maze.Interface;

namespace Maze
{
    public sealed class GreenBonus : InteractiveObject, IRotator
    {
        private GameObject _spawnBonus;        

        private void Start()
        {
            _spawnBonus = GameObject.Find("====BONUS====").gameObject;
        }


        protected override void Interaction()
        {
            //RandomBonus();
            _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();
            _spawnBonus.GetComponent<Bonus>().RandomBonus();
            Destroy(gameObject);
        }


        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == "Bomb" || other.tag == "Bonus")
        //    {
        //        _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();
        //        Destroy(gameObject);
        //    }
        //}

        //private void RandomBonus()
        //{
        //    var _healt = _player.GetComponent<Healthbar>();
        //    _healt._playerHealth += 33.5f;

        //}



        public void Rotation()
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }

    }
}
