using System.Collections.Generic;

namespace SkyStrike.Editor
{
    public interface IDataList<T> : IEditor where T : IData
    {
        public void GetList(out List<T> list);
        public void CreateEmpty(out T data);
        public void Add(T data);
        public void Remove(T data);
        public void Remove(int index, out T data);
        public void Set(int index, T data);
    }
}