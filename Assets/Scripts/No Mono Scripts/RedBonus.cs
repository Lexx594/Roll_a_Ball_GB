using UnityEngine;
using Maze.Interface;

namespace Maze
{
    public sealed class RedBonus : InteractiveObject, IFlay, IRotator
    {        
        private float _lengthFlay;        

        private void Start()
        {            
            _lengthFlay = Random.Range(0.5f, 1.0f);
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
        //    Invoke(nameof(ReturnBonus), 0.5f);
        //    Destroy(gameObject);                       
        //}

        //void ReturnBonus()
        //{
        //    _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();
        //}


        public void Flay()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
            0.5f + Mathf.PingPong(Time.time, _lengthFlay),
            transform.localPosition.z);
        }
        public void Rotation()
        {
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        }
    }

}
