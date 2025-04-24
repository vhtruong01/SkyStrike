using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipMovement : MonoBehaviour, IMoveable
    {
        private float speedX = 5f;
        private float speedY = 4f;
        private float boundX;
        private float boundY;
        private Joystick joystick;
        public UnityAction<EEntityAction> notifyAction { get; set; }
        public bool canMove { get; set; }

        public void Awake()
        {
            Camera cam = Camera.main;
            joystick = FindAnyObjectByType<Joystick>();
            boundX = cam.ScreenToWorldPoint(new(Screen.width, 0)).x;
            boundY = cam.ScreenToWorldPoint(new(0, Screen.height)).y;
            canMove = true;
        }
        public void Update()
        {
            if (canMove)
                Move();
        }
        public void Move()
        {
            Vector2 move = joystick.Direction * Time.deltaTime;
            transform.position = new(Mathf.Clamp(move.x * speedX + transform.position.x, -boundX, boundX),
                                 Mathf.Clamp(move.y * speedY + transform.position.y, -boundY, boundY),
                                 transform.position.z);
        }
        public void Interrupt() => canMove = false;
    }
}