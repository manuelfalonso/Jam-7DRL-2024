namespace JAM.FSM
{
    public interface ITransition
    {
        IState TargetState { get; }
        IPredicate Condition { get; }
    }
}
//EOF.