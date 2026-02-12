using _Scripts.DataBase.Scripts.Enums;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.Data
{
    [CreateAssetMenu(fileName = "CastleData", menuName = "Scriptable Objects/Data/CastleData")]
    public class CastleData : ScriptableObject
    {
        public string castleName;
        public string description;
        public Sprite castleSprite;
        public ECastleType castleType;
        
        // Stats
        public float health;
        public float damage;
        public float rotationSpeed;
        public float fireRate;
    }
}