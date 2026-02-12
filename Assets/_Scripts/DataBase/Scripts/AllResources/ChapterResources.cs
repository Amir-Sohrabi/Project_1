using System.Collections.Generic;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.DataBase.Scripts.AllResources
{
    [CreateAssetMenu(fileName = "ChapterResources", menuName = "Scriptable Objects/Resources/ChapterResources")]
    public class ChapterResources : ScriptableObject
    {
        public List<ChapterData> chapterData = new();
    }
}