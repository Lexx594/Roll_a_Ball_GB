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
        public CameraController cameraController;

        // Start is called before the first frame update
        void Start()
        {

        }
        bool f;
        // Update is called once per frame
        void Update()
        {
            _currentHealthBar.fillAmount = _playerHealth/100;
            if (_playerHealth < 0f && !f)
            {
                f = true;
                Invoke(nameof(LoadLosScene), 5f);
                cameraController.f = true;
                gameObject.GetComponent<PlayerBall>().LossRobot();

            }

            if (_playerHealth > 100f)
            {
                _playerHealth = 100f;
            }           
        }

        public void LoadLosScene() { SceneManager.LoadScene(3); }


        //private void OnTriggerEnter(Collider other)
        //{
        //    //Debug.Log($" Столкновение с {other.name}");

        //    if (other.tag == "Enemy" && !_freezeHealth && !gameObject.GetComponent<PlayerBall>()._isDisembodied)
        //    {
        //        _playerHealth -= 33.5f;
        //        FreezeHealthOnAvtoOff();
        //    }            
        //}

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
