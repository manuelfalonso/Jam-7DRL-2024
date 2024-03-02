using System;

namespace BossRushJam2024.FSM
{
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }

        public bool Evaluate() => _func.Invoke();
    }
}
//EOF.