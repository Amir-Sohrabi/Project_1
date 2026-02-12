using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.AllResources
{
    [CreateAssetMenu(fileName = "WaveResources", menuName = "Scriptable Objects/Resources/WaveResources")]
    public class WaveResources : ScriptableObject
    {
        public List<WaveData> waveData = new();
    }
}