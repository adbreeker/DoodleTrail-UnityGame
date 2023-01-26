using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

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
        if(SceneManager.GetActiveScene().name == "Endless")
        {
            EndlessBehaviors();
        }
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


    // Endless

    void EndlessBehaviors()
    {
        //ReverseLine(line);
        //SimplifyLine(line);
        DeleteOldBrush(line);
        MakeNewBrush(line);
    }

    void DeleteOldBrush(LineRenderer lineRenderer)
    {
        if(lineRenderer.positionCount >= 2)
        {
            Camera cam = FindObjectOfType<Camera>();
            if (lineRenderer.GetPosition(lineRenderer.positionCount - 1).x < cam.transform.position.x - 12)
            {
                Destroy(gameObject);
            }
        }
    }

    void ReverseLine(LineRenderer lineRenderer) // deprecated
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        Array.Reverse(positions);
        lineRenderer.SetPositions(positions);
    }

    void SimplifyLine(LineRenderer lineRenderer) // deprecated
    {
        if(lineRenderer.positionCount > 1000)
        {
            lineRenderer.Simplify(0.001f);
        }
    }

    void MakeNewBrush(LineRenderer lineRenderer)
    {
        if(lineRenderer.positionCount > 5)
        {
            FindObjectOfType<Drawer>().ResetBrush();
        }
    }

    
}
