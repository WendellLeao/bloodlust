namespace Bloodlust.Gameplay.Health
{
    public interface IHasBlood
    {
        float DrainPower { get; }
        
        bool IsBeingDrained { get; set; }
        
        void DrainBlood(int amount, IDamageable damageable);

        IDamageable GetDamageable();
    }
}