using _Scripts.DataBase.Scripts.AllResources;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.Data
{
    [CreateAssetMenu(fileName = "ChapterData", menuName = "Scriptable Objects/Data/ChapterData")]
    public class ChapterData : ScriptableObject
    {
        public int id;
        public int season;
        public string chapterName;
        public string seasonName;
        public string description;
        public Sprite icon;
        public WaveResources waveResources;
    }
}