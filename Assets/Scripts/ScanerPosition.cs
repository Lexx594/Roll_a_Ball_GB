using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanerPosition : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position;
    }
}
