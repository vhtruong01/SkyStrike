namespace SkyStrike.Game
{
    public sealed class SpecialObjectManager : ObjectManager<SpecialObject, SpecialObjectMetaData>
    {
        protected override void Subscribe()
        {
            base.Subscribe();
            EventManager.Subscribe<SpecialObjectEventData>(CreateObject);
        }
        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            EventManager.Unsubscribe<SpecialObjectEventData>(CreateObject);
        }
    }
}