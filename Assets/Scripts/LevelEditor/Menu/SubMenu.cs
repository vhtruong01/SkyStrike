namespace SkyStrike.Editor
{
    public abstract class SubMenu<T> : Menu, IObserver where T : IEditor
    {
        protected T data;
        public bool CanDisplay() => data != null;
        public abstract void BindData();
        public abstract void UnbindData();
        protected override void Preprocess() { }
        public void Display(T data)
        {
            if (!SetData(data)) return;
            UnbindData();
            if (!CanDisplay())
                Hide();
            else BindData();
        }
        public bool SetData(T data)
        {
            if (Equals(this.data, data)) return false;
            this.data = data;
            return true;
        }
        public override void Show()
        {
            if (CanDisplay())
                base.Show();
            else Hide();
        }
    }
}