using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyStrike
{
    namespace Game
    {
        public class Movement : MonoBehaviour
        {
            [SerializeField] private Joystick joystick;
            private float speedX = 3f;
            private float speedY = 2f;
            private float boundX;
            private float boundY;
            private InputAction moveAction;

            public void Awake()
            {
                Camera cam = Camera.main;
                boundX = cam.ScreenToWorldPoint(new(Screen.width * 7 / 8, 0)).x;
                boundY = cam.ScreenToWorldPoint(new(0, Screen.height * 9 / 10)).y;
                PlayerInput playerInput = GetComponent<PlayerInput>();
                moveAction = playerInput.actions["move"];
            }
            public void OnEnable()
            {
                moveAction.Enable();
            }
            public void OnDisable()
            {
                moveAction.Disable();
            }
            public void Update()
            {
                //Vector2 move = moveAction.ReadValue<Vector2>() * Time.deltaTime;
                Vector2 move = joystick.Direction * Time.deltaTime;
                Vector3 newPos = new(Mathf.Clamp(move.x * speedX + transform.position.x, -boundX, boundX)
                                             , Mathf.Clamp(move.y * speedY + transform.position.y, -boundY, boundY)
                                             , transform.position.z);
                transform.position = newPos;
            }
        }

    }
}