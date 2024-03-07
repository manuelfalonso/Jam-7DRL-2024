namespace JAM.Interfaces
{
    public interface IHealable
    {
        bool TryHeal(HealData data);
    }
}
