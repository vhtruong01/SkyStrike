using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IDataList<T> : IData
        {
            public List<T> GetList();
            public T Create();
            public void Remove(T data);
            public void Swap(int leftIndex,int rightIndex);
        }
    }
}