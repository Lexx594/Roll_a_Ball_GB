using Maze;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

namespace MazeSave
{
    public class SaveLoad : MonoBehaviour
    {
        public List<SaveableItemObject> itemObjects = new List<SaveableItemObject>();
        public List<SaveableEnemyObject> enemyObjects = new List<SaveableEnemyObject>();
        
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _enemySpawn;
        [SerializeField] private GameObject _goodBonusBlockPref;
        [SerializeField] private GameObject _badBonusBlockPref;
        [SerializeField] private GameObject _bombPref;
        [SerializeField] private GameObject _activeBombPref;
        [SerializeField] private GameObject _deathDronPref;
        [SerializeField] private GameObject _deathDron2Pref;
        [SerializeField] private GameObject _markPref;
        [SerializeField] private GameObject _enemyDronPref;
        [SerializeField] private TextMeshProUGUI _textSaveCount;
        [SerializeField] private UnityEngine.UI.Button _saveBtn;
        public bool replayGame;

        private SerializableXMLData<SavedData> _serializableXMLData = new SerializableXMLData<SavedData>();
        public int saveCount;

        string path = Path.Combine(Application.streamingAssetsPath, "Save_game.xml");
        string pathReplayGame = Path.Combine(Application.streamingAssetsPath, "SaveReplayGame.xml");

        private void Awake()
        {
            replayGame = DataHolder.replayGame;
        }



        private void Start()
        {
            saveCount = DataHolder.saveCount;
            _textSaveCount.text = saveCount.ToString();
            if (!replayGame)
            {
                SaveGame(pathReplayGame);
            }
            else
            {
                Replay();
                replayGame = false;
                DataHolder.replayGame = replayGame;
            }


            
            
        }

        void Update()
        {
            if (saveCount > 0)
            {
                _saveBtn.image.color = Color.white;
            }
            else _saveBtn.image.color = Color.red;


            //Debug.Log(itemObjects.Count);
            //Debug.Log(enemyObjects.Count);
        }


        public void GamingSave()
        { 
            if (saveCount > 0)
            {
                saveCount--;
                _textSaveCount.text = saveCount.ToString();
                SaveGame(path);               
                
            }        
            
        }

        public void Replay() { LoadGame(pathReplayGame); }

        public void GamingLoad() { LoadGame(path); }

        public void SaveGame(string path)
        {
            PlayerData PlayerData = new PlayerData()
            {
                name = _player.name,
                position = _player.transform.position,
                rotation = _player.transform.localEulerAngles,
                velocity = _player.GetComponent<Rigidbody>().velocity,
                angularVelocity = _player.GetComponent<Rigidbody>().angularVelocity,
                health = _player.GetComponent<Healthbar>()._playerHealth,
                isFreezeHealth = _player.GetComponent<Healthbar>()._freezeHealth,
                isDisembodied = _player.GetComponent<PlayerBall>()._isDisembodied,
                bomb = _player.GetComponent<Inventory>().bombs,
                map = _player.GetComponent<Inventory>().map,
                scaner = _player.GetComponent<Inventory>().scaner,
                mark = _player.GetComponent<Inventory>().marks
            };

            List<ItemData> ItemDatas = new List<ItemData>();
            for (int i = 0; i < itemObjects.Count; i++)
            {
                ItemDatas.Add(new ItemData()
                {
                    //name = itemObjects[i].GetComponent<SaveableItemObject>().objectName,
                    name = itemObjects[i].name,
                    position = itemObjects[i].transform.position,
                });
            }

            List<EnemyData> EnemyDatas = new List<EnemyData>();

            for (int i = 0; i < enemyObjects.Count; i++)
            {
                EnemyDatas.Add(new EnemyData()
                {                   
                    name = enemyObjects[i].name,
                    //name = enemyObjects[i].GetComponent<SaveableEnemyObject>().objectName,
                    position = enemyObjects[i].transform.position,
                    rotation = enemyObjects[i].transform.localEulerAngles,
                    nextPointPosition = enemyObjects[i].GetComponent<Enemy>().walkPoint
                });
            }

            SavedData _savedData = new SavedData()
            {
                PlayerData = PlayerData,
                ItemDatas= ItemDatas,
                EnemyDatas= EnemyDatas,
                EnemyCount = _enemySpawn.GetComponent<EnemySpawn>().enemyCount,
                LeftKillEnemy = _enemySpawn.GetComponent<EnemySpawn>().leftKillEnemy,
                SaveCount = saveCount
            };

            
            _serializableXMLData.Save(_savedData, path);
        }

        public void LoadGame(string path)
        {
            //var path = Path.Combine(Application.streamingAssetsPath, "Save_game.xml");


            SavedData _savedData =  _serializableXMLData.Load(path);
            //Debug.Log(_savedData);

            if (_savedData != null)
            {
                DestroyAllObject();

                _player.transform.position = _savedData.PlayerData.position;
                _player.transform.localEulerAngles = _savedData.PlayerData.rotation;
                _player.GetComponent<Rigidbody>().velocity = _savedData.PlayerData.velocity;
                _player.GetComponent<Rigidbody>().angularVelocity = _savedData.PlayerData.angularVelocity;
                _player.GetComponent<Healthbar>()._playerHealth = _savedData.PlayerData.health;
                _player.GetComponent<Healthbar>()._freezeHealth = _savedData.PlayerData.isFreezeHealth;
                _player.GetComponent<PlayerBall>()._isDisembodied = _savedData.PlayerData.isDisembodied;
                _player.GetComponent<Inventory>().bombs = _savedData.PlayerData.bomb;
                _player.GetComponent<Inventory>().map = _savedData.PlayerData.map;
                _player.GetComponent<Inventory>().scaner = _savedData.PlayerData.scaner;
                _player.GetComponent<Inventory>().marks = _savedData.PlayerData.mark;

                for (int i = 0; i < _savedData.ItemDatas.Count; i++)
                {
                    if (_savedData.ItemDatas[i].name == "Good bonus block(Clone)")
                    {
                        Instantiate(_goodBonusBlockPref, _savedData.ItemDatas[i].position, Quaternion.identity);
                    }
                    else if (_savedData.ItemDatas[i].name == "Bad bonus block(Clone)")
                    {
                        Instantiate(_badBonusBlockPref, _savedData.ItemDatas[i].position, Quaternion.identity);
                    }
                    else if (_savedData.ItemDatas[i].name == "Bomb(Clone)")
                    {
                        Instantiate(_bombPref, _savedData.ItemDatas[i].position, Quaternion.identity);
                    }
                    else if (_savedData.ItemDatas[i].name == "Active Bomb(Clone)")
                    {
                        Instantiate(_activeBombPref, _savedData.ItemDatas[i].position, Quaternion.identity);
                    }
                    else if (_savedData.ItemDatas[i].name == "Death dron(Clone)")
                    {
                        Instantiate(_deathDronPref, _savedData.ItemDatas[i].position, Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up));
                    }
                    else if (_savedData.ItemDatas[i].name == "Death dron 2(Clone)")
                    {
                        Instantiate(_deathDron2Pref, _savedData.ItemDatas[i].position, Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360), Vector3.up));
                    }
                    else if (_savedData.ItemDatas[i].name == "Mark(Clone)")
                    {
                        Instantiate(_markPref, _savedData.ItemDatas[i].position, Quaternion.LookRotation(Vector3.down));
                    }
                }

                for (int i = 0; i < _savedData.EnemyDatas.Count; i++)
                {
                    if (_savedData.EnemyDatas[i].name == "Enemy(Clone)")
                    {
                        //Debug.Log("Нашли дрона");
                        var instobj = Instantiate(_enemyDronPref, _savedData.EnemyDatas[i].position, Quaternion.identity) as GameObject;
                        if (instobj != null) 
                        {
                            //Debug.Log("Создали дрона");
                            instobj.transform.localEulerAngles = _savedData.EnemyDatas[i].rotation;
                            instobj.GetComponent<Enemy>().walkPoint = _savedData.EnemyDatas[i].nextPointPosition;
                        }
                    }
                }
                _enemySpawn.GetComponent<EnemySpawn>().enemyCount = _savedData.EnemyCount;
                _enemySpawn.GetComponent<EnemySpawn>().leftKillEnemy = _savedData.LeftKillEnemy;
                _enemySpawn.GetComponent<EnemySpawn>().CheckKillEnemy();
                saveCount = _savedData.SaveCount;
                _textSaveCount.text = saveCount.ToString();
            }
        }

        void DestroyAllObject()
        {
            for (int i = 0; i < itemObjects.Count; i++)
            {                
                Destroy(itemObjects[i].gameObject);                
            }
            for (int i = 0; i < enemyObjects.Count; i++)
            {                
                Destroy(enemyObjects[i].gameObject);
            }
        }
    }
}
