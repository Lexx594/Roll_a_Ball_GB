using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Maze;

namespace MazeMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _quit;
        [SerializeField] private Slider _sliderVolume;
        [SerializeField] private int _sceneID;
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private TextMeshProUGUI _textDropdown;

        [SerializeField] private Image _loadingImage;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private GameObject _loadScene;

        [SerializeField] private AudioSource _audsClick;


        private float _moveSpeed = 10f;
        private int _maxEnemys = 7;
        private int _minEnemys = 4;
        private int _maxBonus = 12;
        private int _minBonus = 8;
        private int _maxBombs = 12;
        private int _minBombs = 9;
        private int _bombs = 5;
        private int _map = 1;
        private int _scaner = 0;
        private int _saveCount = 2;



        private void Awake()
        {
            AudioListener.volume = _sliderVolume.value;
            _sliderVolume.onValueChanged.AddListener(value => AudioListener.volume = value);
            _play.onClick.AddListener(PlayGame);
            _quit.onClick.AddListener(Quit);
            TextDropdown();
        }

        public void PlayClick()
        {
            _audsClick.Play();
        }

        public void Quit()
        {
            PlayClick();
            Application.Quit();
        }

        void PlayGame()
        {
            PlayClick();
            DataHolder.moveSpeed = _moveSpeed;
            DataHolder.maxEnemys = _maxEnemys;
            DataHolder.minEnemys = _minEnemys;
            DataHolder.maxBonus = _maxBonus;
            DataHolder.minBonus = _minBonus;
            DataHolder.maxBombs = _maxBombs;
            DataHolder.minBombs = _minBombs;
            DataHolder.bombs = _bombs;
            DataHolder.map = _map;
            DataHolder.scaner = _scaner;
            DataHolder.saveCount = _saveCount;
            _loadScene.SetActive(true);
            StartCoroutine(AsyncLoad());            
        }

        IEnumerator AsyncLoad()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_sceneID);
            while (!operation.isDone)
            {
                float progress = operation.progress / 0.9f;
                _loadingImage.fillAmount = progress;
                _progressText.text = string.Format("{0:0}%", progress * 100);
                yield return null;
            }
        }

        public void DropdownSample()
        {
            int index = _dropdown.value;
            switch (index)
            {
                case 0:
                    {
                        _moveSpeed = 13f;
                        _maxEnemys = 5;
                        _minEnemys = 3;
                        _maxBonus = 15;
                        _minBonus = 10;
                        _maxBombs = 10;
                        _minBombs = 7;
                        _bombs = 10;
                        _map = 1;
                        _scaner = 1;
                        _saveCount = 3;
                        TextDropdown();
                        break; 
                    }
                case 1:
                    {
                        _moveSpeed = 10f;
                        _maxEnemys = 7;
                        _minEnemys = 4;
                        _maxBonus = 12;
                        _minBonus = 8;
                        _maxBombs = 12;
                        _minBombs = 9;
                        _bombs = 5;
                        _map = 1;
                        _scaner = 0;
                        _saveCount = 2;
                        TextDropdown();
                        break;
                    }                    

                case 2:
                    {
                        _moveSpeed = 8f;
                        _maxEnemys = 10;
                        _minEnemys = 6;
                        _maxBonus = 10;
                        _minBonus = 6;
                        _maxBombs = 15;
                        _minBombs = 10;
                        _bombs = 2;
                        _map = 0;
                        _scaner = 0;
                        _saveCount = 1;
                        TextDropdown();
                        break;
                    }
            }
        }

        void TextDropdown()
        {
            string moveSpeed;
            if (_moveSpeed == 13f) moveSpeed = "сильное";
            else if (_moveSpeed == 10f) moveSpeed = "среднее";
            else moveSpeed = "слабое";
            string map;
            map = _map == 1 ? "есть" : "нет";
            string scaner;
            scaner = _scaner == 1 ? "есть" : "нет";


            _textDropdown.text = $"Потивников - {_minEnemys}-{_maxEnemys}\n" +
                $"Бомб на карте - {_minBombs}-{_maxBombs}\n" +
                $"Бонусов на карте - {_minBonus}-{_maxBonus}\n" +
                $"Ускорение игрока - {moveSpeed}\n" +
                $"Бомб в инвентаре - {_bombs}\n" +
                $"Карта в инвентаре - {map}\n" +
                $"Сканер в инвентаре - {scaner}\n" +
                $"Количество сохранений - {_saveCount}";
        }
    }
}
