namespace BossRushJam2024.Interfaces
{
    public interface IHealable
    {
        bool TryHeal(HealData data);
    }
}
