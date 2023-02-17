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
                SceneManager.LoadScene(3);
            }

            if (_playerHealth > 100f)
            {
                _playerHealth = 100f;
            }           
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($" Столкновение с {other.name}");

            if (other.tag == "Enemy" && !_freezeHealth && !gameObject.GetComponent<PlayerBall>()._isDisembodied)
            {
                _playerHealth -= 33.5f;
                FreezeHealthOnAvtoOff();
            }            
        }

        public void FreezeHealthOn()
        {
            _freezeHealth = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void FreezeHealthOnAvtoOff()
        {
            _freezeHealth = true;
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            Invoke(nameof(FreezeHealthOff), 10f);
        }


        public void FreezeHealthOff()
        { 
            _freezeHealth = false;
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
