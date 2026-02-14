using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.AllResources
{
    [CreateAssetMenu(fileName = "TroopResources", menuName = "Scriptable Objects/Resources/TroopResources")]
    public class TroopResources : ScriptableObject
    {
        public List<TroopData> troopData = new();
    }
}