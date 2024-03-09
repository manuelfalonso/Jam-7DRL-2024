namespace JAM.Systems.Abilities
{
    public class AbilityData
    {
        public bool Executed;
        public bool IsOnUseTime;
        public bool IsOnCooldown;

        public bool CanBeExecuted => !IsOnUseTime && !IsOnCooldown;
    }
}
