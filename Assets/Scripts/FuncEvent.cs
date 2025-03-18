using System;

namespace SkyStrike
{
    public class FuncEvent<T>
    {
        private Func<T> func;
        public void AddListener(Func<T> f) => func = f;
        public void RemoveAllListeners() => func = null;
        public T Invoke() => func.Invoke();
    }
    public class FuncEvent<P, R>
    {
        private Func<P, R> func;
        public void AddListener(Func<P, R> f) => func = f;
        public void RemoveAllListeners() => func = null;
        public R Invoke(P val) => func.Invoke(val);
    }
}