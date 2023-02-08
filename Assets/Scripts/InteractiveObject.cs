using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        protected virtual void Interaction()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Interaction();
            }
        }
    }
}
