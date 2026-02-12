using System;

namespace _Scripts.DataBase.Scripts.Enums
{
    [Serializable]
    public enum EEnemyType : byte
    {
        None = 0,
        Gunner,
        Bumper,
        Grappler,
        FrontSuicideBomber,
        MotorbikeLancer,
        FrontMotorbikeLancer
    }
}