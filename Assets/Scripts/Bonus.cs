using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;


namespace Maze
{
    public class Bonus : MonoBehaviour
    {
        [SerializeField] private GameObject _effects;
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _message;
        [SerializeField] private TextMeshProUGUI _timerLabel;
        private float _timerTime;


        private void Start()
        {            
            _player = GameObject.Find("Player").gameObject;
        }

        private void Update()
        {
            if (_timerTime > 0f)
            {
                _effects.transform.GetChild(1).gameObject.SetActive(true);
                _timerTime -= Time.deltaTime;
                _timerLabel.text = $"{Mathf.Round(_timerTime)} сек";
            }
            if (_timerTime < 1f)
            {                
                _effects.transform.GetChild(1).gameObject.SetActive(false);
                ResetBonus();
                _timerTime = -1f;
            }
            //Debug.Log(_timerTime);
        }



        public void RandomBonus()
        {

            int numder = Random.Range(0, 3);
            Debug.Log(numder);
            switch (numder)
            {
                case 0:
                    Health();
                    break;
                
                case 1:                    
                    Boost();
                    break;

                case 2:                    
                    Slowing();
                    break;

                case 3:
                    //

                    break;


            }




        }
        private void Health()
        {
            var healt = _player.GetComponent<Healthbar>();
            healt._playerHealth += 33.5f;
            _message.gameObject.SetActive(true);
            _message.GetComponent<TextMeshProUGUI>().text = Message("здоровье");

        }





        private void Boost()
        {
            _timerTime = 30f;
            _player.GetComponent<PlayerBall>().moveSpeed = 6f;
            _message.gameObject.SetActive(true);
            _message.GetComponent<TextMeshProUGUI>().text = Message("ускорение");
            _effects.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ускорение";
        }

        private void Slowing()
        {
            _timerTime = 30f;
            _player.GetComponent<PlayerBall>().moveSpeed = 2f;
            _message.gameObject.SetActive(true);
            _message.GetComponent<TextMeshProUGUI>().text = Message("замедление");
            _effects.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            _effects.transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Замедление";
        }


        private void ResetBonus()
        {

            _player.GetComponent<PlayerBall>().moveSpeed = 4f;
            _effects.transform.GetChild(0).gameObject.SetActive(false);
            _effects.transform.GetChild(1).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(false);
        }



        private string Message(string bonusName)
        {
            Invoke(nameof(ExitMessage), 2.5f);
            string _mess = $"Вы подобрали {bonusName}";
            return _mess;
        }
        
        private void ExitMessage() { _message.gameObject.SetActive(false);}



    }
}
