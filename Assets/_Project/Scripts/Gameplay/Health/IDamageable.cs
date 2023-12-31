namespace Bloodlust.Gameplay.Health
{
    public interface IDamageable
    {
        int CurrentHealth { get; }
        
        int MaxHealth { get; }
        
        int OriginalMaxHealth { get; }
        
        bool HealthWillDeplete { get; }

        void TakeDamage(int amount);

        void Heal(int amount);

        void HealMaxHealth(int amount);

        void SetIsInvulnerable(bool isInvulnerable);
    }
}