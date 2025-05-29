using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class SpecialObject : ObjectEntity<SpecialObjectMetaData>
    {
        private SpriteAnimation anim;

        public override void Awake()
        {
            base.Awake();
            anim = GetComponent<SpriteAnimation>();
            EnableCollider(false);
        }
        protected override IEnumerator Prepare(float delay)
        {
            anim.SetData(data.metaData.sprites);
            anim.Play();
            IMoveable movement = GetComponentInChildren<IMoveable>();
            yield return new WaitForSeconds(delay);
            MoveData moveData = data.moveData;
            while (data.pointIndex < moveData.points.Length)
            {
                MoveData.Point point = moveData.points[data.pointIndex];
                yield return StartCoroutine(movement.Travel(point.standingTime));
                data.pointIndex++;
            }
            if (!data.isMaintain)
            {
                anim.Stop();
                Disappear();
            }
        }
    }
}