using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public abstract class InteractiveObject : MonoBehaviour
    {

        public GameObject _spawnBonus;
         
        protected virtual void Interaction()
        {
            Destroy(gameObject);
        }

        //protected virtual void CollisionOfTwoObjects()
        //{
        //    //Destroy(gameObject);
        //}

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($" Столкновение с {other.tag}");

            if (other.tag == "Player")
            {
                Interaction();
            }

            //if (other.tag == "Bomb" || other.tag == "Bonus")
            //{
            //    throw new Exception("два интерактивных объекта в одной точке");
                
            //    //CollisionOfTwoObjects();                                
            //}
        }
    }
}
