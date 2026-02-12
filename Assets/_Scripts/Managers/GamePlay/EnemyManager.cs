using System.Collections.Generic;
using _Scripts.Abstractions.Service;
using _Scripts.DataBase;
using _Scripts.DataBase.Scripts.Data;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Managers.GamePlay
{
    public class EnemyManager : SingletonMonoBehaviour<EnemyManager>, IManager
    {
        // Data to read from
        private readonly List<EnemyData> _copiedEnemyData = new();

        public void InitializeManager()
        {
            GameData.Instance.EnemyResources.enemyData.ForEach(e =>
            {
                var enemyData = ScriptableObject.CreateInstance<EnemyData>();
                enemyData.enemyName = e.enemyName;
                enemyData.description = e.description;
                enemyData.enemySprite = e.enemySprite;
                enemyData.enemyType = e.enemyType;

                // Stats
                enemyData.health = e.health;
                enemyData.damage = e.damage;
                enemyData.speed = e.speed;

                _copiedEnemyData.Add(enemyData);
            });
        }
    }
}