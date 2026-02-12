namespace _Scripts.Abstractions.GamePlay
{
    public interface IAttacker
    {
        void Attack(IKillable target);
    }
}