namespace SkyStrike.Game
{
    public sealed class SpecialObjectManager : ObjectManager<SpecialObject, SpecialObjectMetaData>
    {
        protected override void Subcribe()
            => EventManager.Subscribe<SpecialObjectEventData>(CreateObject);
        protected override void Unsubcribe()
            => EventManager.Unsubscribe<SpecialObjectEventData>(CreateObject);
    }
}