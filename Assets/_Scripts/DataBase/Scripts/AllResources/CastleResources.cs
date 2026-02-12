using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.AllResources
{
    [CreateAssetMenu(fileName = "CastleResources", menuName = "Scriptable Objects/Resources/CastleResources")]
    public class CastleResources : ScriptableObject
    {
        public List<CastleData> castleData = new();
    }
}
