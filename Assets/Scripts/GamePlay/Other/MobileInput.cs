using UnityEngine;

namespace SkyStrike.Game
{
    public class MobileInput : Input
    {
        private Joystick joystick;
        public override Vector2 Direction => joystick.Direction;

        public void Awake()
            => joystick = GetComponentInChildren<Joystick>();
        public override void Active() => joystick.gameObject.SetActive(true);
        public override void Deactive() => joystick.gameObject.SetActive(false);
    }
}