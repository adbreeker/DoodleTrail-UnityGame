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

    void Awake()
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

        GetComponent<CircleCollider2D>().offset = line.GetPosition(0);
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
        edgeCollider.offset = new Vector2(0, 0);
    }


    // Endless ----------------------------------------------------------------------------------------------- Endless

    void EndlessBehaviors()
    {
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

    void MakeNewBrush(LineRenderer lineRenderer)
    {
        if(lineRenderer.positionCount > 5)
        {
            FindObjectOfType<Drawer>().ResetBrush();
        }
    }

    
}
