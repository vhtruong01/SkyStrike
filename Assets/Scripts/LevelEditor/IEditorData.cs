using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IEditorData
        {
            public IGameData ToGameData();
        }
    }
}