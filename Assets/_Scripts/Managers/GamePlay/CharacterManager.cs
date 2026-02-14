using System.Collections.Generic;
using _Scripts.Abstractions.Service;
using _Scripts.DataBase;
using _Scripts.DataBase.Scripts.Data;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Managers.GamePlay
{
    public class CharacterManager : SingletonMonoBehaviour<CharacterManager>, IManager
    {
        private readonly List<CharacterData> _copiedCharacterData = new();

        public void InitializeManager()
        {
            GameData.Instance.CharacterResources.characterData.ForEach(c =>
            {
                var characterData = ScriptableObject.CreateInstance<CharacterData>();
                characterData.characterName = c.characterName;
                characterData.description = c.description;
                characterData.characterSprite = c.characterSprite;
                characterData.characterType = c.characterType;

                // Stats
                characterData.health = c.health;
                characterData.damage = c.damage;
                characterData.speed = c.speed;
                characterData.fireRate = c.fireRate;

                _copiedCharacterData.Add(characterData);
            });
        }
    }
}