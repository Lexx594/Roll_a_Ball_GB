using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Maze;
using Maze.Interface;
using TMPro;

namespace MazeMenu
{
    public class LosMenu : MonoBehaviour
    {

        //[SerializeField] private Button _quit;
        //[SerializeField] private Button _replay;
        [SerializeField] private GameObject _loadScene;
        [SerializeField] private AudioSource _audsClick;

        [SerializeField] private int _sceneID;
        [SerializeField] private Image _loadingImage;
        [SerializeField] private TextMeshProUGUI _progressText;
        public bool replayGame;

        //private void Awake()
        //{
        //    _quit.onClick.AddListener(Quit);
        //    _replay.onClick.AddListener(ReplayGame);
        //}

        public void Quit()
        {
            _loadScene.SetActive(true);
            _audsClick.Play();
            _sceneID = 1;
            StartCoroutine(AsyncLoad());
        }

        public void ReplayGame()
        { 
            _audsClick.Play();
            _loadScene.SetActive(false);
            replayGame = true;
            DataHolder.replayGame = replayGame;
            _sceneID = 2;
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
    }
}
