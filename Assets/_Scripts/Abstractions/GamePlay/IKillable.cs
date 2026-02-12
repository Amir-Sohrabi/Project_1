namespace _Scripts.Abstractions.GamePlay
{
    public interface IKillable
    {
        void TakeDamage(float damageAmount);
        void Die();
    }
}