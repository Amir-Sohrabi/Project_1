using System;

namespace _Scripts.DataBase.Scripts.Inventory
{
    [Serializable]
    public class PlayerInventory
    {
        public int currentChapter;

        public PlayerInventory(PlayerInventory inventory = null)
        {
            if (inventory == null) return;
            currentChapter = inventory.currentChapter;
        }
    }
}