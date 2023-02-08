using UnityEngine;
using Maze.Interface;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Maze
{
    public sealed class RedBonus : InteractiveObject, IFlay, IRotator
    {
        
        private float _lengthFlay;
        private GameObject _spawnBonus;


        private void Start()
        {            
            _lengthFlay = Random.Range(0.5f, 1.0f);
            _spawnBonus = GameObject.Find("====BONUS====").gameObject;
        }
        
        
        
        
        protected override void Interaction()
        {
            _spawnBonus.GetComponent<BonusSpawn>().AddNewBonus();
            _spawnBonus.GetComponent<Bonus>().RandomBonus();
            Destroy(gameObject);
        }
       
        
        
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
