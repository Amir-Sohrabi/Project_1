using System;

namespace _Scripts.DataBase.Scripts.Inventory
{
    [Serializable]
    public class UserProfile
    {
        public PlayerData playerData;
        
        //Currency balance related
        public long earnedGem;
        public long spentGem;
        public long earnedCoin;
        public long spentCoin;
    }
}