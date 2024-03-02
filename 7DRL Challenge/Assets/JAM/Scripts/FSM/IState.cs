namespace BossRushJam2024.FSM
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}
//EOF.