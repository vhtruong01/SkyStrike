namespace SkyStrike.Game
{
    public class SpecialObjectMovement : ObjectMovement
    {
        public void Awake()
        {
            entity = GetComponent<IObject>();
            entityMoveData = GetComponent<IEntityMoveData>();
        }
        public override void Move() { }
        public override void Stop() { }
    }
}