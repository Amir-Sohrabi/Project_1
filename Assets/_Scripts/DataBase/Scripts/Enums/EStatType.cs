using System;

namespace _Scripts.DataBase.Scripts.Enums
{
    [Serializable]
    public enum EStatType : byte
    {
        None = 0,
        Damage,
        FireRate,
        ReloadTime,
        Health,
        WeightAndHandling,
        CollisionDamage,
        Fuel,
        Gun,
        Accuracy,
        PelletDamage,
        PelletCount,
        DamagePerSecond,
        FlameDamagePerSecond,
        BurnDamagePerSecond,
        BurnDuration,
        Range,
        AnchorWeight,
        BombDamage
    }
}