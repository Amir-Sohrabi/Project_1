using System;
using _Scripts.DataBase.Scripts.Enums;

namespace _Scripts.DataBase.Scripts.Items
{
    [Serializable]
    public class CharacterItem
    {
        public ECharacterType characterType;
        public int level;
    }
}