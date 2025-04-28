using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public class ShipMovement : MonoBehaviour, IShipComponent, IMoveable
    {
        private float boundX;
        private float boundY;
        private Joystick joystick;
        public IEntity entity { get; set; }
        public ShipData data { get; set; }
        public UnityAction<EEntityAction> notifyAction { get; set; }

        public void Init()
        {
            Camera cam = Camera.main;
            joystick = FindAnyObjectByType<Joystick>();
            boundX = cam.ScreenToWorldPoint(new(Screen.width, 0)).x;
            boundY = cam.ScreenToWorldPoint(new(0, Screen.height)).y;
            entity.transform.localScale = Vector3.one;
            entity.position = Vector3.zero;
        }
        private void Update()
        {
            if (data.canMove)
                Move();
        }
        public void Move()
        {
            Vector2 dir = joystick.Direction * Time.deltaTime;
            entity.position = new(Mathf.Clamp(dir.x * data.speed + entity.position.x, -boundX, boundX),
                                  Mathf.Clamp(dir.y * 0.75f * data.speed + entity.position.y, -boundY, boundY),
                                  entity.position.z);
        }
        public void Interrupt() => data.canMove = false;
    }
}