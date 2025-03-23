using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IDataList<T> where T : class
        {
            public List<T> GetList();
            public T CreateEmpty();
            public void Add(T data);
            public void Remove(T data);
            public void Remove(int index);
            public void Swap(int leftIndex, int rightIndex);
            public void Set(int index, T data);
        }
    }
}