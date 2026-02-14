using _Scripts.DataBase.Scripts.Enums;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public string description;
        public Sprite enemySprite;
        public EEnemyType enemyType;

        // Stats
        public float health;
        public float damage;
        public float speed;
        public float fireRate;
    }
}
