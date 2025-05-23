namespace SkyStrike.Game
{
    public interface IState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}