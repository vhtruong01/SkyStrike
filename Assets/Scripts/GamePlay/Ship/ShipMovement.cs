using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class ShipMovement : MonoBehaviour, IShipComponent, IMoveable
    {
        private float boundX;
        private float boundY;
        private IInput input;
        private IAnimation anim;
        public IEntity entity { get; set; }
        public ShipData shipData { get; set; }

        public void Init()
        {
            Camera cam = Camera.main;
            boundX = cam.ScreenToWorldPoint(new(Screen.width, 0)).x;
            boundY = cam.ScreenToWorldPoint(new(0, Screen.height)).y;
            entity.transform.localScale = Vector3.one;
            entity.position = Vector3.zero;
            anim = GetComponentInChildren<IAnimation>(true);
            input = FindAnyObjectByType<Input>(FindObjectsInactive.Include);
            if (input == null)
                Debug.LogError("No input");
        }
        private void Update()
        {
            if (!shipData.canMove || input == null) return;
            Vector2 dir = input.Direction * Time.deltaTime;
            entity.position = new(Mathf.Clamp(dir.x * shipData.speed + entity.position.x, -boundX, boundX),
                                  Mathf.Clamp(dir.y * 0.75f * shipData.speed + entity.position.y, -boundY, boundY),
                                  entity.position.z);
        }
        public IEnumerator Travel(float delay)
        {
            if (delay > 0)
            {
                anim.Stop();
                yield return new WaitForSeconds(delay);
            }
            Move();
        }
        public void Move()
        {
            anim.Play();
            shipData.canMove = true;
            input?.Active();
        }
        public void Stop()
        {
            anim.Stop();
            shipData.canMove = false;
            input?.Deactive();
        }
        public void Interrupt() => Stop();
    }
}