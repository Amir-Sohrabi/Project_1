using System.Collections.Generic;
using _Scripts.Abstractions.Service;
using _Scripts.DataBase;
using _Scripts.DataBase.Scripts.Data;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Managers.GamePlay
{
    public class CastleManager : SingletonMonoBehaviour<CastleManager>, IManager
    {
        private readonly List<CastleData> _copiedCastleData = new();

        public void InitializeManager()
        {
            GameData.Instance.CastleResources.castleData.ForEach(c =>
            {
                var castleData = ScriptableObject.CreateInstance<CastleData>();
                castleData.castleName = c.castleName;
                castleData.description = c.description;
                castleData.castleSprite = c.castleSprite;
                castleData.castleType = c.castleType;

                // Stats
                castleData.health = c.health;
                castleData.damage = c.damage;
                castleData.rotationSpeed = c.rotationSpeed;
                castleData.fireRate = c.fireRate;

                _copiedCastleData.Add(castleData);
            });
        }
    }
}