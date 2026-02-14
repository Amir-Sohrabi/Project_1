using _Scripts.Abstractions.GamePlay;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.Core.Base
{
    public class BaseTroop : MonoBehaviour, IAttacker, IKillable
    {
        // Data
        protected TroopData _troopData;

        public void Initialize(TroopData troopData)
        {
            _troopData = troopData;
        }

        public void Attack(IKillable target)
        {
            target.TakeDamage(_troopData.damage);
        }

        public void TakeDamage(float damageAmount)
        {
            if (_troopData.health <= damageAmount)
            {
                _troopData.health = 0;
                Die();
                return;
            }

            _troopData.health -= damageAmount;
        }

        public void Die()
        {
            // Death effect
            // Death animation
            // Back to pool
        }
    }
}