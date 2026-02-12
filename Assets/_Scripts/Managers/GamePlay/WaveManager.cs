using System.Collections.Generic;
using _Scripts.Abstractions.Service;
using _Scripts.DataBase;
using _Scripts.DataBase.Scripts.Data;
using _Scripts.Utils;

namespace _Scripts.Managers.GamePlay
{
    public class WaveManager : SingletonMonoBehaviour<WaveManager>, IManager
    {
        private readonly List<WaveData> _copiedWaveData = new();

        public void InitializeManager()
        {
            GameData.Instance.WaveResources.waveData.ForEach(w =>
            {
                var waveData = new WaveData
                {
                    waveBossData = w.waveBossData,
                    waveEnemyData = w.waveEnemyData
                };

                _copiedWaveData.Add(waveData);
            });
        }
        
        
    }
}