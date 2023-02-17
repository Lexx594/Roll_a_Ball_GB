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

        [SerializeField] private Button _quit;
        [SerializeField] private GameObject _loadScene;
        [SerializeField] private AudioSource _audsClick;

        [SerializeField] private int _sceneID;
        [SerializeField] private Image _loadingImage;
        [SerializeField] private TextMeshProUGUI _progressText;

        private void Awake()
        {
            _quit.onClick.AddListener(Quit);            
        }

        public void Quit()
        {
            _audsClick.Play();
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
