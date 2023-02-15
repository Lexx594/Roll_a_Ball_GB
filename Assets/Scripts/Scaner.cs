using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Maze
{

    public class Scaner : MonoBehaviour
    {
        [SerializeField] private Transform _scanerPingPrefab;
        [SerializeField] private LayerMask _scanerlayerMask;
        [SerializeField] private AudioSource _audsScanerActive;
        [SerializeField] private TextMeshProUGUI _scaerLevelLabel;

        [SerializeField] private Transform _pulseTransform;
        private float _range;
        private float _maxRange = 10;
        public bool scanerOff = true;
        public int levelScaner = 1;

        void Start()
        {
            _pulseTransform = transform.Find("Pulse");            
        }

        void Update()
        {
            if(scanerOff)
            {
                if (levelScaner == 1) _maxRange = 4; 
                else if (levelScaner == 2) _maxRange = 7;
                else if (levelScaner == 3) _maxRange = 10;
                
                _scaerLevelLabel.text = levelScaner.ToString();

                float rangeSpeed = _maxRange;
                _range += rangeSpeed * Time.deltaTime;
                if ( _range > _maxRange )
                {
                    _range = 0f;
                    ScanerOn();
                    _audsScanerActive.Play();                
                }
                _pulseTransform.localScale = new Vector3(_range, _range);   

            }            
        }

        void ScanerOn()
        {
            Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _maxRange*2.5f, _scanerlayerMask);
            for (int i = 0; i < overlappedColliders.Length; i++)
            {
                Transform radarPingTransform = Instantiate(_scanerPingPrefab, overlappedColliders[i].transform.position + Vector3.up * 2f, Quaternion.LookRotation(Vector3.down));
                RadarPing radarPing = radarPingTransform.GetComponent<RadarPing>();
                if (overlappedColliders[i].gameObject.GetComponent<PlayerBall>() != null)
                {
                    //это игрок
                    radarPing.SetColor(Color.green);
                }
                else if (overlappedColliders[i].gameObject.GetComponent<Bomb>() != null)
                {
                    //это бомба
                    radarPing.SetColor(new Color(1, 0, 1));
                }

                else if (overlappedColliders[i].gameObject.GetComponent<GreenBonus>() != null
                    || overlappedColliders[i].gameObject.GetComponent<RedBonus>() != null)
                {
                    //это враг
                    radarPing.SetColor(new Color(0, 0, 1));
                }
                else
                {
                    radarPing.SetColor(new Color(1, 0, 0));
                }



            }
        }
    }
}
