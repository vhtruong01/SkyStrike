using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Editor
{
    public class BulletObject : MonoBehaviour
    {
        private float elapsedTime;
        private float duration;
        private float speed;
        private float defaultSpeed;
        private int index;
        private float startScale;
        private float endScale;
        private Vector3 velocity;
        public List<BulletStateDataObserver> states;
        public UnityEvent<BulletObject> onDestroy { get; private set; }

        public void Init() => onDestroy = new();
        public void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            elapsedTime += deltaTime;
            if (elapsedTime < duration)
            {
                transform.position += velocity * (speed * deltaTime);
                if (endScale != startScale)
                    transform.localScale = Vector3.one * Lerp(startScale, endScale, elapsedTime / duration);
            }
            else ChangeState();
        }
        private float Lerp(float a, float b, float t)
            => a + (b - a) * t;
        private void ChangeState()
        {
            if (index >= states.Count)
            {
                onDestroy.Invoke(this);
                return;
            }
            var stateData = states[index];
            float angle = stateData.rotation.data * Mathf.Deg2Rad;
            if (angle != 0)
            {
                float sin = Mathf.Sin(angle);
                float cos = Mathf.Cos(angle);
                velocity = new(velocity.x * cos - velocity.y * sin, velocity.x * sin + velocity.y * cos, velocity.z);
            }
            elapsedTime = 0;
            duration = stateData.duration.data;
            speed = stateData.coef.data * defaultSpeed;
            startScale = stateData.scale.data;
            index++;
            if (index >= states.Count)
                endScale = startScale;
            else endScale = states[index].scale.data;
        }
        public void Init(BulletDataObserver bulletData, Vector2 dir, Vector2 pos)
        {
            bulletData.GetList(out states);
            defaultSpeed = bulletData.speed.data;
            velocity = dir;
            transform.position = pos.SetZ(transform.position.z);
            if (bulletData.isUseState.data && states.Count > 0)
            {
                index = 0;
                ChangeState();
            }
            else
            {
                elapsedTime = 0;
                duration = bulletData.lifetime.data;
                speed = defaultSpeed;
                startScale = endScale = bulletData.size.data;
                transform.localScale = Vector3.one * startScale;
                velocity *= defaultSpeed;
            }
        }
    }
}