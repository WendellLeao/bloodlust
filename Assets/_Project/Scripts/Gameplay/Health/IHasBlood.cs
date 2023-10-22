namespace Bloodlust.Gameplay.Health
{
    public interface IHasBlood
    {
        float DrainPower { get; }
        
        void DrainBlood(int amount, IDamageable damageable);

        IDamageable GetDamageable();
    }
}