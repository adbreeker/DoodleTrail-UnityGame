using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class BrushBehavior : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    LineRenderer line;

    void Start()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        line = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        SetEdgeCollider(line);
        DeleteOldBrush(line);
    }

    void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for(int point = 0; point < lineRenderer.positionCount; point++)
        {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

    void DeleteOldBrush(LineRenderer lineRenderer)
    {
        if(lineRenderer.positionCount >= 3)
        {
            Camera cam = FindObjectOfType<Camera>();
            if (lineRenderer.GetPosition(lineRenderer.positionCount - 1).x < cam.transform.position.x - 20)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "LineEraser")
        {
            Destroy(gameObject);
        }
    }
}
