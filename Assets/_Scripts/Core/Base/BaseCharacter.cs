using _Scripts.Abstractions.GamePlay;
using _Scripts.DataBase.Scripts.Data;
using UnityEngine;

namespace _Scripts.Core.Base
{
    public abstract class BaseCharacter : MonoBehaviour, IAttacker, IKillable
    {
        // Data
        private CharacterData _characterData;

        protected float Speed => _characterData != null ? _characterData.speed : 0f;
        
        public void Initialize(CharacterData characterData)
        {
            _characterData = characterData;
        }
        
        public void Attack(IKillable target)
        {
            target.TakeDamage(_characterData.damage);
        }
        
        public void TakeDamage(float damageAmount)
        {
            if (_characterData.health <= damageAmount)
            {
                _characterData.health = 0;
                Die();
                return;
            }
            
            _characterData.health -= damageAmount;
        }

        public void Die()
        {
            // Death effect
            // Death animation
            // Back to pool
        }
    }
}
