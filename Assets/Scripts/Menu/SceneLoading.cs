using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoading : MonoBehaviour
{

    [SerializeField] private int _sceneID; 
    
    [SerializeField] private Image _loadingImage;
    [SerializeField] private TextMeshProUGUI _progressText;

    void Start()
    {
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
