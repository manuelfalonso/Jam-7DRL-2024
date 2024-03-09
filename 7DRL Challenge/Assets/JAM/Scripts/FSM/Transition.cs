namespace JAM.FSM
{ 
    public class Transition : ITransition
    {
        public IState TargetState { get; }
        public IPredicate Condition { get; }

        public Transition(IState to, IPredicate condition)
        {
            TargetState = to;
            Condition = condition;
        }
    }
}
//EOF.