using _Scripts.Abstractions.GamePlay;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.Core.Base
{
    public abstract class BaseEnemy : MonoBehaviour, IAttacker, IKillable
    {
        // Data
        private EnemyData _enemyData;
        
        public void Initialize(EnemyData enemyData)
        {
            _enemyData = enemyData;
        }
        
        public void Attack(IKillable target)
        {
            target.TakeDamage(_enemyData.damage);
        }
        
        public void TakeDamage(float damageAmount)
        {
            if (_enemyData.health <= damageAmount)
            {
                _enemyData.health = 0;
                Die();
                return;
            }
            
            _enemyData.health -= damageAmount;
        }

        public void Die()
        {
            // Death effect
            // Death animation
            // Back to pool
        }
    }
}