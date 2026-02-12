using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Scripts.DataBase.Scripts.Inventory
{
    [Serializable]
    public class PlayerData
    {
        public PlayerInventory inventory = new();
        
        public void SetPreTutorialData()
        {
            inventory.currentChapter = 0;
        }

        public void ResetData()
        {
            inventory.currentChapter = 0;
        }

        public PlayerInventory DefaultInventory()
        {
            var newInventory = new PlayerInventory()
            {
                currentChapter = 0
            };

            return newInventory;
        }

        public void CreateDefaultData()
        {
            var defaultInventory = DefaultInventory();
            // CustomJson.SaveJson(defaultInventory, GameData.PLAYER_DATA_JSON);
            GameData.Instance.datahub.userProfile.playerData.inventory = defaultInventory;
            // GameData.Instance.datahub.userProfile.mainHallStatLevels = new();
            // GameData.Instance.MainHallResourceData.StatLevels.ResetData();
            // var statLevels = GameData.Instance.MainHallResourceData.StatLevels;
            // GameData.Instance.datahub.userProfile.mainHallStatLevels = statLevels;
            // CustomJson.SaveJson(statLevels, GameData.MAIN_HALL_STAT_JSON);

            // ProgressionSystem.Instance.ResetData();
            // ProgressionSystem.Instance.SaveProgression();
            GameData.Instance.datahub.SaveProfile(CancellationToken.None).Forget();
        }
    }
}