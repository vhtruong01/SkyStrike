using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class ObjectEntity<T> : PoolableObject<ObjectEntityData<T>> where T : ObjectMetaData
    {
        public sealed override void Refresh()
        {
            spriteRenderer.sprite = data.metaData.sprite;
            spriteRenderer.color = data.metaData.color;
            col2D.size = data.metaData.sprite.bounds.size / 1.5f;
            transform.localScale = Vector3.one * data.size;
        }
        public void Launch(float delay)
            => StartCoroutine(Prepare(delay));
        protected abstract IEnumerator Prepare(float delay);
    }
}