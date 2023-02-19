using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


namespace MazeSave
{
    public class SaveableEnemyObject : MonoBehaviour
    {
        public string objectName;
        private SaveLoad _saver;

        private void Awake()
        {
            _saver = FindObjectOfType<SaveLoad>();
        }

        private void Start()
        {
            _saver.enemyObjects.Add(this);
        }

        private void OnDestroy()
        {
            _saver.enemyObjects.Remove(this);
        }        
    }
}
