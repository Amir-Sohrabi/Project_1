using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.AllResources
{
    [CreateAssetMenu(fileName = "EnemyResources", menuName = "Scriptable Objects/Resources/EnemyResources")]
    public class EnemyResources : ScriptableObject
    {
        public List<EnemyData> enemyData = new();
    }
}
