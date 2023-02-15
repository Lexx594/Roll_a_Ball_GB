using Maze;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int bombs = 0;
    public int map = 0;
    public int scaner = 0;
    public int marks = 0;
    

    [SerializeField] private LayerMask _scanerlayerMask;
    [SerializeField] private GameObject _scsnerPingPrefab;
    private float _timerTime;

    

    [SerializeField] private GameObject _activeBombPrefab;
    [SerializeField] private GameObject _markPrefab;
    [SerializeField] private GameObject _inventorySlots;
    [SerializeField] private GameObject _minimap;
    [SerializeField] private GameObject _scaner;
    [SerializeField] private TextMeshProUGUI _inventoryLabel;
    [SerializeField] private TextMeshProUGUI _timerLabel;

    [SerializeField] private Camera _miniMapCamera;

    [SerializeField] private AudioSource _audsMark;
    [SerializeField] private AudioSource _audsScaner;
    //[SerializeField] private AudioSource _audsScanerActive;
    [SerializeField] private AudioSource _audsMap;
      

    private bool _miniMapActive;
    private bool _scanerRecharge;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && bombs > 0) PutABomb();
        if (Input.GetKeyDown(KeyCode.M) && map > 0) Map();
        if (Input.GetKeyDown(KeyCode.F) && scaner > 0 && !_scanerRecharge) Scaner();
        if (Input.GetKeyDown(KeyCode.T) && marks > 0) Marker();

        if (bombs > 0)
        {
            _inventorySlots.transform.GetChild(0).gameObject.SetActive(true);
            _inventorySlots.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bombs.ToString();
        }
        else _inventorySlots.transform.GetChild(0).gameObject.SetActive(false);

        if (map > 0) _inventorySlots.transform.GetChild(3).gameObject.SetActive(true);
        else _inventorySlots.transform.GetChild(3).gameObject.SetActive(false);

        if (scaner > 0) _inventorySlots.transform.GetChild(2).gameObject.SetActive(true);
        else _inventorySlots.transform.GetChild(2).gameObject.SetActive(false);

        if (marks > 0)
        {
            _inventorySlots.transform.GetChild(1).gameObject.SetActive(true);
            _inventorySlots.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = marks.ToString();
        }
        else _inventorySlots.transform.GetChild(1).gameObject.SetActive(false);


        if (bombs > 0 || map > 0 || scaner > 0 || marks > 0)
        {
            _inventoryLabel.gameObject.SetActive(true);
        }
        else _inventoryLabel.gameObject.SetActive(false);

        if (map > 1) map = 1;

        if (scaner == 1) _scaner.GetComponent<Scaner>().levelScaner = 1;
        else if (scaner == 2) _scaner.GetComponent<Scaner>().levelScaner = 2;
        else if (scaner == 3) _scaner.GetComponent<Scaner>().levelScaner = 3;
        else if (scaner > 3) scaner = 3;

            //Таймер сканера
            if (_timerTime > 0f)
        {            
            _timerTime -= Time.deltaTime;
            _timerLabel.color = Color.yellow;
            _timerLabel.text = $"{Mathf.Round(_timerTime)} сек";           
        }
        if (_timerTime < 1f && _timerTime > 0f && _scanerRecharge)
        {
            _timerTime = -1f;
            RechargeScaner();
        }
    }

    public void PutABomb()
    {
        Instantiate(_activeBombPrefab, transform.position, Quaternion.identity);
        bombs -= 1;
    }

    private void Map()
    {
        if (!_miniMapActive)
        {
            _minimap.gameObject.SetActive(true);
            _miniMapActive = true;
            _audsMap.Play();
        }
        else
        {
            _minimap.gameObject.SetActive(false);
            _miniMapActive = false;
            _audsMap.Play();
        }
    }

    private void Scaner()
    {
        _timerTime = 60f;
        if (map == 0)
        {
            _minimap.gameObject.SetActive(true);
            _miniMapActive = true;
            _miniMapCamera.cullingMask = 1 << 9; //| 1<<7;
        }
        else
        { 
            _miniMapCamera.cullingMask = 1 << 0 | 1 << 9;
            _minimap.gameObject.SetActive(true);
            _miniMapActive = true;
            _audsMap.Play();
        }
        _timerLabel.gameObject.SetActive(true);
        _scaner.GetComponent<Scaner>().scanerOff = true;
        _scanerRecharge = true;
         
        _audsScaner.Play();      

    }

    private void RechargeScaner()
    {
        _scaner.GetComponent<Scaner>().scanerOff = false;
        if (map == 0)
        {
            _minimap.gameObject.SetActive(false);
            _miniMapActive = false;
            _miniMapCamera.cullingMask = 1 << 4; 
        }
        else _miniMapCamera.cullingMask = 1 << 0; 
        _miniMapCamera.cullingMask = 1;        
        _timerLabel.color = Color.red;
        _timerLabel.text = "Перезарядка сканера";
        Invoke(nameof(ScanerReady), 120f);
    }

    private void ScanerReady()
    {
        _scanerRecharge = false;
        _audsScaner.Play();
        
        _timerLabel.gameObject.SetActive(false);
    }

    private void Marker()
    {
        marks -= 1;
        _audsMark.Play();
        Instantiate(_markPrefab, transform.position - Vector3.up * 0.49f, Quaternion.LookRotation(Vector3.down));                
    }

}
