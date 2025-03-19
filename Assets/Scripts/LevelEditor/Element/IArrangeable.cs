namespace SkyStrike
{
    namespace Editor
    {
        public interface IArrangeable
        {
            public void MoveLeft();
            public void MoveRight();
            public void Create();
            public void Remove();
            public void Duplicate();
        }
    }
}