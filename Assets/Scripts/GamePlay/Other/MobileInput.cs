using UnityEngine;

namespace SkyStrike.Game
{
    public class MobileInput : Input
    {
        [SerializeField] private Joystick joystick;
        public override Vector2 Direction => joystick.Direction;

        public override void Active() => joystick.gameObject.SetActive(true);
        public override void Deactive() => joystick.gameObject.SetActive(false);
    }
}