using SkyStrike.Game;

namespace SkyStrike.Editor
{
    public interface IEditorData<T, N> : ICloneable<N>, IEditor where T : IGame, new() where N : IEditor
    {
        public T ExportData();
        public void ImportData(T data);
    }
}