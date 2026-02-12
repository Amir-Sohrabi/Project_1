using System;
using System.Collections.Generic;

namespace _Scripts.DataBase.Scripts.Data
{
    [Serializable]
    public class WaveData
    {
        public List<WaveBossData>  waveBossData = new();
        public List<WaveEnemyData> waveEnemyData = new();
    }
}