using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Maze
{
    public class PausedMenu : MonoBehaviour
    {
        [SerializeField] private Button _newGame;
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _quit;
        [SerializeField] private Button _back;
        [SerializeField] private GameObject _panelMenu;
        [SerializeField] private Slider _sliderVolume;

        [SerializeField] private AudioSource _audsClick;
        private bool _isPaused = false;

        private void Awake()
        {
            AudioListener.volume = _sliderVolume.value;
            _sliderVolume.onValueChanged.AddListener(value => AudioListener.volume = value);

            _newGame.onClick.AddListener(NewGame);
            _quit.onClick.AddListener(Quit);
            _mainMenu.onClick.AddListener(MainMenu);
            _back.onClick.AddListener(Resume);
        }

        public void PlayClick()
        {
            _audsClick.Play();
        }

        public void Quit()
        {
            Resume();
            Application.Quit();
        }

        public void NewGame()
        {
            Resume();
            SceneManager.LoadScene(2);
        }

        public void MainMenu()
        {
            Resume();
            SceneManager.LoadScene(1);
        }


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (!_isPaused) Pause();
                else Resume();
            }
        }

        public void Resume()
        {            
            _panelMenu.SetActive(false);
            _isPaused = false;
            Time.timeScale = 1f;
            AudioListener.volume = _sliderVolume.value;
            PlayClick();
        }

        public void Pause()
        {
            _isPaused = true;
            _panelMenu.SetActive(true);
            Time.timeScale = 0f;
            AudioListener.volume = 0f;
        }
    }
}
