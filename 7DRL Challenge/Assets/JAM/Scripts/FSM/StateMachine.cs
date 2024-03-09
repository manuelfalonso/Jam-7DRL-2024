using System;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.FSM
{
    public partial class StateMachine : MonoBehaviour
    {
        StateNode _currentStateNode;
        Dictionary<Type, StateNode> _nodesDict = new();
        HashSet<ITransition> _anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                ChangeState(transition.TargetState);
            }

            _currentStateNode.State?.Update();
        }

        public void FixedUpdate()
        {
            _currentStateNode.State?.FixedUpdate();
        }

        public void SetState(IState state)
        {
            _currentStateNode = _nodesDict[state.GetType()];
            _currentStateNode.State?.OnEnter();
        }

        void ChangeState(IState state)
        {
            if (state == _currentStateNode.State) { return; }

            var previousState = _currentStateNode.State;
            var nextState = _nodesDict[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            _currentStateNode = _nodesDict[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (!transition.Condition.Evaluate()) { continue; }
                return transition;
            }

            foreach (var transition in _currentStateNode.Transitions)
            {
                if (!transition.Condition.Evaluate()) { continue; }
                return transition;
            }

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        StateNode GetOrAddNode(IState state)
        {
            var node = _nodesDict.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodesDict.Add(state.GetType(), node);
            }

            return node;
        }
    }
}
//EOF.