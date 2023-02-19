using System;
using System.Collections.Generic;
using UnityEngine;

namespace MazeSave
{
    [Serializable]
    public sealed class SavedData
    {
        public PlayerData PlayerData;
        public List<ItemData> ItemDatas = new List<ItemData>();
        public List<EnemyData> EnemyDatas = new List<EnemyData>();
        public int EnemyCount;
        public int LeftKillEnemy;
        public int SaveCount;
    }
    [Serializable]
    public class PlayerData
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public float health;
        public bool isFreezeHealth;
        public bool isDisembodied;
        public int bomb;
        public int map;
        public int scaner;
        public int mark;
    }

    [Serializable]
    public class ItemData
    {
        public string name;
        public Vector3 position;        
    }

    [Serializable]
    public class EnemyData
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 nextPointPosition;
    }


    [Serializable]
    public struct Vector3Serializable
    {
        public float X;
        public float Y;
        public float Z;
        private Vector3Serializable(float valueX, float valueY, float valueZ)
        {
            X = valueX;
            Y = valueY;
            Z = valueZ;
        }
        public static implicit operator Vector3(Vector3Serializable value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }
        public static implicit operator Vector3Serializable(Vector3 value)
        {
            return new Vector3Serializable(value.x, value.y, value.z);
        }
        public override string ToString() => $" (X = {X} Y = {Y} Z = {Z})";
    }

}
