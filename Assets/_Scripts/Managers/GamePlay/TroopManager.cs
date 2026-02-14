using System.Collections.Generic;
using _Scripts.Abstractions.Service;
using _Scripts.DataBase;
using _Scripts.DataBase.Scripts.Data;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Managers.GamePlay
{
    public class TroopManager : SingletonMonoBehaviour<TroopManager>, IManager
    {
        private readonly List<TroopData> _copiedTroopData = new();

        public void InitializeManager()
        {
            GameData.Instance.TroopResources.troopData.ForEach(c =>
            {
                var troopData = ScriptableObject.CreateInstance<TroopData>();
                troopData.troopName = c.troopName;
                troopData.description = c.description;
                troopData.troopSprite = c.troopSprite;
                troopData.enemyType = c.enemyType;

                // Stats
                troopData.health = c.health;
                troopData.damage = c.damage;
                troopData.speed = c.speed;
                troopData.fireRate = c.fireRate;

                _copiedTroopData.Add(troopData);
            });
        }
    }
}