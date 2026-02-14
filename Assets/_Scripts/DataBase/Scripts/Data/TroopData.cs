using _Scripts.DataBase.Scripts.Enums;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.Data
{
    [CreateAssetMenu(fileName = "TroopData", menuName = "Scriptable Objects/Data/TroopData")]
    public class TroopData : ScriptableObject
    {
        public string troopName;
        public string description;
        public Sprite troopSprite;
        public ETroopType enemyType;

        // Stats
        public float health;
        public float damage;
        public float speed;
        public float fireRate;
    }
}