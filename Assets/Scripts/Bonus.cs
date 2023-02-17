using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        [SerializeField] private AudioSource _audsGoodBonus;
        [SerializeField] private AudioSource _audsBadBonus;
        [SerializeField] private AudioSource _audsAddItem;

        private void Start()
        {            
            _player = GameObject.Find("Player").gameObject;
            ResetBonus();
        }

        private void Update()
        {
            if (_timerTime > 0f)
            {
                _effects.transform.GetChild(1).gameObject.SetActive(true);
                _timerTime -= Time.deltaTime;
                _timerLabel.text = $"{Mathf.Round(_timerTime)} сек";
            }
            if (_timerTime < 1f && _timerTime > 0f)
            {                
                _effects.transform.GetChild(1).gameObject.SetActive(false);
                ResetBonus();
                _timerTime = -1f;
            }
            //Debug.Log(_timerTime);
        }

        public void RandomBonus()
        {

            int numder = Random.Range(0, 10);
            //Debug.Log(numder);
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
                    Safety();
                    break;

                case 4:
                    Disembodied();
                    break;

                case 5:
                    AddBomb();
                    break;

                case 6:
                    AddMap();
                    break;

                case 7:
                    AddScaner();
                    break;

                case 8:
                    AddMark();
                    break;

                case 9:
                    AddActiveBomb();
                    break;

            }

        }
        private void Health()
        {
            _audsAddItem.Play();
            var healt = _player.GetComponent<Healthbar>();
            healt._playerHealth += 33.5f;
            _message.GetComponent<TextMeshProUGUI>().text = Message("здоровье");
        }

        private void Boost()
        {
            ResetBonus();
            _audsGoodBonus.Play();
            _timerTime = 30f;
            _player.GetComponent<PlayerBall>().moveSpeed = DataHolder.moveSpeed * 1.5f;
            _message.GetComponent<TextMeshProUGUI>().text = Message("ускорение");
            _effects.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Ускорение";
        }

        private void Slowing()
        {
            ResetBonus();
            _audsBadBonus.Play();
            _timerTime = 30f;
            _player.GetComponent<PlayerBall>().moveSpeed = DataHolder.moveSpeed/2;
            _message.GetComponent<TextMeshProUGUI>().text = Message("замедление");
            _effects.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            _effects.transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Замедление";
        }

        private void Safety()
        {
            ResetBonus();
            _audsGoodBonus.Play();
            _timerTime = 20f;
            _player.GetComponent<Healthbar>().FreezeHealthOn();
            _message.GetComponent<TextMeshProUGUI>().text = Message("неуязвимость");
            _effects.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
            _effects.transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Неуязвимость";
        }

        private void Disembodied()
        {
            ResetBonus();
            _timerTime = 15f;
            _audsGoodBonus.Play();
            _player.GetComponent<PlayerBall>()._isDisembodied = true;            
            _message.GetComponent<TextMeshProUGUI>().text = Message("безтелесность");
            _effects.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(true);
            _effects.transform.GetChild(0).gameObject.SetActive(true);
            _effects.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Безтелесность";
        }

        private void ResetBonus()
        {
            _player.GetComponent<PlayerBall>().moveSpeed = DataHolder.moveSpeed;
            _effects.transform.GetChild(0).gameObject.SetActive(false);
            _effects.transform.GetChild(1).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
            _effects.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(false);
            _player.GetComponent<Healthbar>().FreezeHealthOff();
            _player.GetComponent<PlayerBall>()._isDisembodied = false;
        }

        private void AddBomb()
        {
            _message.GetComponent<TextMeshProUGUI>().text = Message("бомбу");
            _player.GetComponent<Inventory>().bombs += 3;
            _audsAddItem.Play();
        }
        private void AddMap()
        {
            _message.GetComponent<TextMeshProUGUI>().text = Message("карту");
            _player.GetComponent<Inventory>().map += 1;
            _audsAddItem.Play();
        }
        private void AddScaner()
        {
            _message.GetComponent<TextMeshProUGUI>().text = Message("сканер");
            _player.GetComponent<Inventory>().scaner += 1;
            _audsAddItem.Play();
        }
        private void AddMark()
        {
            _message.GetComponent<TextMeshProUGUI>().text = Message("маркер");
            _player.GetComponent<Inventory>().marks += 5;
            _audsAddItem.Play();
        }
        private void AddActiveBomb()
        {
            _message.GetComponent<TextMeshProUGUI>().text = Message("зажженную бомбу");
            _player.GetComponent<Inventory>().bombs += 1;
            _player.GetComponent<Inventory>().PutABomb();
            _audsBadBonus.Play();
        }

        private string Message(string bonusName)
        {
            _message.gameObject.SetActive(true);
            Invoke(nameof(ExitMessage), 2.5f);
            string _mess = $"Вы подобрали {bonusName}";
            return _mess;
        }
        
        private void ExitMessage() { _message.gameObject.SetActive(false);}



    }
}
