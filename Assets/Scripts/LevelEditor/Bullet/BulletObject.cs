using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Editor
{
    public class BulletObject : MonoBehaviour
    {
        private float elapsedTime;
        private float duration;
        private float startCoef;
        private float endCoef;
        private float defaultSpeed;
        private float transitionDuration;
        private int index;
        private float startScale;
        private float endScale;
        private Vector3 velocity;
        private Vector3 scale;
        public List<BulletStateDataObserver> states;
        public UnityEvent<BulletObject> onDestroy { get; private set; }

        public void Init()
        {
            onDestroy = new();
            scale = transform.localScale;
        }
        public void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            elapsedTime += deltaTime;
            float remainTime = duration - elapsedTime;
            if (remainTime > 0)
            {
                if (remainTime >= transitionDuration)
                    transform.position += velocity * (defaultSpeed * startCoef * deltaTime);
                else transform.position += velocity * (defaultSpeed * Lerp(endCoef, startCoef, remainTime / transitionDuration) * deltaTime);
                if (endScale != startScale)
                    transform.localScale = scale * Lerp(startScale, endScale, elapsedTime / duration);
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
            startCoef = stateData.coef.data;
            startScale = stateData.scale.data;
            duration = stateData.duration.data;
            transitionDuration = Mathf.Min(stateData.transitionDuration.data, duration);
            index++;
            if (index >= states.Count)
            {
                endScale = startScale;
                endCoef = startCoef;
            }
            else
            {
                endScale = states[index].scale.data;
                endCoef = states[index].coef.data;
            }
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
                startCoef = endCoef = 1;
                startScale = endScale = bulletData.size.data;
                transitionDuration = 0;
            }
            transform.localScale = scale * startScale;
        }
    }
}