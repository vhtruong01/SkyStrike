using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyStrike
{
    namespace Ship
    {
        public class Attack : MonoBehaviour
        {
            public bool isAttack { get; private set; }
            private InputAction attackAction;
            private BulletManager bulletManager;

            public void Awake()
            {
                bulletManager = GetComponent<BulletManager>();
                PlayerInput playerInput = GetComponent<PlayerInput>();
                attackAction = playerInput.actions["attack"];
            }
            public void Start()
            {
                Enable(true);
            }
            public void OnEnable()
            {
                attackAction.performed += SetAutoFire;
                attackAction.Enable();
            }
            public void OnDisable()
            {
                attackAction.performed -= SetAutoFire;
                attackAction.Disable();
            }
            public void SetAction(InputAction action) => attackAction = action;
            public void SetAutoFire(InputAction.CallbackContext ctx)
            {
                Enable(!isAttack);
            }
            public void Enable(bool enabled)
            {
                isAttack = enabled;
                if (isAttack) bulletManager?.Shoot();
                else bulletManager?.StopShoot();
            }
        }
    }
}