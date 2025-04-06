using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShipTrail : MonoBehaviour
{
    private readonly float shipVelocityY = 4f;
    private readonly float maxTrailLen = 0.5f;
    private LineRenderer trail;
    private LinkedList<Vector3> trailVertexPositions;

    public void Awake()
    {
        trailVertexPositions = new();
        trail = GetComponent<LineRenderer>();
        trail.useWorldSpace = true;
    }

    public void Update()
    {
        trailVertexPositions.AddFirst(transform.position);
        trail.positionCount = 1;
        trail.SetPosition(0, transform.position);
        var curTrailVertex = trailVertexPositions.First.Next;
        float shipVelocityX = 0;
        if (trailVertexPositions.Count > 1)
        {
            shipVelocityX = trailVertexPositions.First.Value.x - trailVertexPositions.First.Next.Value.x;
        }
        int trailVertexIndex = 0;
        while (curTrailVertex != null)
        {
            trailVertexIndex++;
            Vector3 newPos = curTrailVertex.Value;
            newPos.y -= Time.deltaTime * shipVelocityY;
            newPos.x += shipVelocityX / trailVertexIndex;
            curTrailVertex.Value = newPos;
            if (Mathf.Abs(trailVertexPositions.First.Value.y - newPos.y) > maxTrailLen)
            {
                while (trailVertexPositions.Last != curTrailVertex)
                    trailVertexPositions.RemoveLast();
                trailVertexPositions.RemoveLast();
                break;
            }
            trail.positionCount = trailVertexIndex + 1;
            trail.SetPosition(trailVertexIndex, newPos);
            curTrailVertex = curTrailVertex.Next;
        }
    }
}