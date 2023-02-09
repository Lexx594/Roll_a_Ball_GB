using UnityEngine;
using Maze.Interface;

namespace Maze
{
    public sealed class GreenBonus : InteractiveObject, IRotator
    {
        
        private void Start()
        {
            _spawnBonus = GameObject.Find("====BONUS====").gameObject;
            _spawnBonus.GetComponent<GameController>()._interactiveObjects = FindObjectsOfType<InteractiveObject>();
        }

        protected override void Interaction()
        {            
            _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();
            _spawnBonus.GetComponent<Bonus>().RandomBonus();
            Destroy(gameObject);
        }
        //protected override void CollisionOfTwoObjects()
        //{
        //    Destroy(gameObject);
        //    _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();            
        //}

        protected override void CollisionOfTwoObjects()
        {
            Invoke(nameof(ReturnBonus), 0.5f);
            Destroy(gameObject);
        }

        void ReturnBonus()
        {
            _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();
        }




        public void Rotation()
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }

    }
}
