using _Scripts.Managers.GamePlay;
using _Scripts.Utils;
using UnityEngine;

namespace _Scripts.Bootstrap
{
    /// GameBootstrapper is where the managers should be initialized.
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private CastleManager castleManager;

        private void Awake()
        {
            enemyManager.InitializeManager();
            waveManager.InitializeManager();
            castleManager.InitializeManager();
            
            DontDestroyOnLoad(gameObject);
        }
    }
}