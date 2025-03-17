namespace SkyStrike
{
    namespace Enemy
    {
        public interface IActionData 
        {
            public string GetActionDataInfo(int index);
            public IActionData Clone();
        }
    }
}