using _Scripts.DataBase.Scripts.AllResources;
using _Scripts.DataBase.Scripts.Inventory;
using _Scripts.Utils;
using Unity.Collections;
using UnityEngine;

namespace _Scripts.DataBase
{
    public class GameData : SingletonMonoBehaviour<GameData>
    {
        // Assigned in inspector
        [SerializeField] private CastleResources castleResources;
        [SerializeField] private ChapterResources chapterResources;
        [SerializeField] private CharacterResources characterResources;
        [SerializeField] private EnemyResources enemyResources;
        [SerializeField] private WaveResources waveResources;
        
        // Used by others
        public CastleResources CastleResources => castleResources;
        public ChapterResources ChapterResources => chapterResources;
        public CharacterResources CharacterResources => characterResources;
        public EnemyResources EnemyResources => enemyResources;
        public WaveResources WaveResources => waveResources;
        
        [HideInInspector] [ReadOnly] public DataHub datahub;
    }
}