using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Maze
{


    public class Healthbar : MonoBehaviour
    {
        public float _playerHealth = 100;
        [SerializeField] private Image _currentHealthBar;        
        public bool _freezeHealth;
        
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _currentHealthBar.fillAmount = _playerHealth/100;
            if (_playerHealth < 0f)
            {
                SceneManager.LoadScene(0);
            }

            if (_playerHealth > 100f)
            {
                _playerHealth = 100f;
            }
           




        }
    }
}
