using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.AllResources
{
    [CreateAssetMenu(fileName = "CharacterResources", menuName = "Scriptable Objects/Resources/CharacterResources")]
    public class CharacterResources : ScriptableObject
    {
        public List<CharacterData> characterData = new();
    }
}