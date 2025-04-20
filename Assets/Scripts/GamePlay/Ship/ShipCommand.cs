using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyStrike.Game
{
    public class ShipCommand : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        private float speedX = 5f;
        private float speedY = 5f;
        private float boundX;
        private float boundY;
        private InputAction moveAction;
        private ShipBulletManager shipBulletManager;

        public void Awake()
        {
            Camera cam = Camera.main;
            boundX = cam.ScreenToWorldPoint(new(Screen.width, 0)).x;
            boundY = cam.ScreenToWorldPoint(new(0, Screen.height)).y;
            PlayerInput playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["move"];
            shipBulletManager = GetComponent<ShipBulletManager>();
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
        public void SetAttackEnabled(bool isEnabled)
            => shipBulletManager.SetActive(isEnabled);
    }

}