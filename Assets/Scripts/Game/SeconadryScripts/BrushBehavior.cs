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

    bool _updateCollider = true;

    void Awake()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        line = this.GetComponent<LineRenderer>();

        edgeCollider.edgeRadius = line.startWidth / 2;

        _updateCollider = true;
    }

    void Update()
    {

        if(SceneManager.GetActiveScene().name == "Endless")
        {
            EndlessBehaviors();
        }

        if(_updateCollider)
        {
            SetEdgeCollider(line);
            GetComponent<CircleCollider2D>().offset = line.GetPosition(line.positionCount / 2);
        }
    }


    void SetEdgeCollider(LineRenderer lineRenderer)
    {
        List<Vector2> edges = new List<Vector2>();

        for(int point = 0; point < lineRenderer.positionCount; point++)
        {

            Vector2 edgePoint = lineRenderer.GetPosition(point);

            //Checking for no collision mask
            if (Physics2D.OverlapPoint(edgePoint, LayerMask.GetMask("NoCollisionMask")))
            {
                for(int backsidePoint = lineRenderer.positionCount - 1; backsidePoint > point; backsidePoint--)
                {
                    edgePoint = lineRenderer.GetPosition(backsidePoint);
                    if (Physics2D.OverlapPoint(edgePoint, LayerMask.GetMask("NoCollisionMask")))
                    {
                        break;
                    }
                    edges.Add(edgePoint);
                }
                break;
            }

            edges.Add(edgePoint);
        }

        if(edges.Count >= 2)
        {
            edgeCollider.SetPoints(edges);
            edgeCollider.offset = new Vector2(0, 0);
        }
    }

    public void StopUpdatingCollider()
    {
        _updateCollider = false;
        SetEdgeCollider(line);
        GetComponent<CircleCollider2D>().offset = line.GetPosition(line.positionCount / 2);
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
            Camera cam = FindFirstObjectByType<Camera>();
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
            FindFirstObjectByType<Drawer>().ResetBrush();
        }
    }

    
}
