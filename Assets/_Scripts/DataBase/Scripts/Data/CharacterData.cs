using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Enums;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        public string description;
        public Sprite characterSprite;
        public ECharacterType characterType;
        
        public float health;
        public float damage;
        public float speed;
    }
}