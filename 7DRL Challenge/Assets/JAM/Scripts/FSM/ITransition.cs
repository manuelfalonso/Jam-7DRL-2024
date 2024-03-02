namespace BossRushJam2024.FSM
{
    public interface ITransition
    {
        IState TargetState { get; }
        IPredicate Condition { get; }
    }
}
//EOF.