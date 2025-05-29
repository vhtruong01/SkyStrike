namespace SkyStrike.Game
{
    public class SpecialObjectMovement : ObjectMovement
    {
        protected override float scale
        {
            get => data.size;
            set => data.size = value;
        }
        private SpecialObjectData data;

        public void Awake()
        {
            data = GetComponent<SpecialObjectData>();
            entity = GetComponent<IObject>();
            entityMoveData = GetComponent<IEntityMoveData>();
        }
        public override void Move() { }
        public override void Stop() { }
    }
}