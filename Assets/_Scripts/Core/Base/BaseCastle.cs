using _Scripts.Abstractions.GamePlay;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.Core.Base
{
    public class BaseCastle : MonoBehaviour, IAttacker, IKillable, IPlaceable
    {
        // Data
        protected CastleData _castleData;

        public void Initialize(CastleData castleData)
        {
            _castleData = castleData;
        }

        public void Attack(IKillable target)
        {
            target.TakeDamage(_castleData.damage);
        }

        public void TakeDamage(float damageAmount)
        {
            if (_castleData.health <= damageAmount)
            {
                _castleData.health = 0;
                Die();
                return;
            }

            _castleData.health -= damageAmount;
        }

        public void Die()
        {
            // Death effect
            // Death animation
            // Back to pool
        }

        public void Place()
        {
            // Get from pool
            // Play placing animation
        }

        public void Replace()
        {
            // Replace with a pooled object
        }
    }
}