namespace SkyStrike
{
    namespace Enemy
    {
        public class FireData : IActionData
        {
            public FireData() { }
            public IActionData Clone()
            {
                return MemberwiseClone() as FireData;
            }
            public string GetActionDataInfo(int index)
            {
                return "";
            }
        }
    }
}