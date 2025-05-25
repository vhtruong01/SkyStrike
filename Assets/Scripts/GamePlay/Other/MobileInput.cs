using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyStrike.Game
{
    public class MobileInput : Input
    {
        private Joystick joystick;
        private InputAction inputAction;
        private float keyboardSpeed;
        private Vector2 dir;

        public override Vector2 Direction => dir;

        public void Awake()
        {
            joystick = GetComponentInChildren<Joystick>();
            var input = GetComponent<PlayerInput>();
            inputAction = input.actions.FindAction("Move");
        }
        public void Update()
        {
            dir = joystick.Direction;
            if (dir.x == 0 && dir.y == 0)
            {
                dir = inputAction.ReadValue<Vector2>();
                if (dir.x == 0 && dir.y == 0)
                {
                    keyboardSpeed = 0;
                    return;
                }
                if (keyboardSpeed < 1)
                    keyboardSpeed += Time.deltaTime * 2f;
                dir *= keyboardSpeed;
            }
        }
        public override void Active() => joystick?.gameObject.SetActive(true);
        public override void Deactive() => joystick?.gameObject.SetActive(false);
    }
}