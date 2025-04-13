using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IDataList<T> : IEditor where T : IEditor
        {
            public void GetList(out List<T> list);
            public void CreateEmpty(out T data);
            public void Add(T data);
            public void Remove(T data);
            public void Remove(int index,out T data);
            public void Set(int index, T data);
            //public void Swap(int leftIndex, int rightIndex);
        }
    }
}