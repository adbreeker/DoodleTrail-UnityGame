using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Drawer : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brushPrefab;
    LineRenderer currentLineRenderer;
    Vector2 lastPos;

    EventSystem eventSystem;
    public int linesCount = 0;
    public bool drawPermision = true;

    List<GameObject> lines = new List<GameObject>();
    GameObject currentLine;


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    void Start()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if(!IsPointerOverUIObject() && drawPermision && Time.timeScale != 0)
        {
            Drawing();
            DestroyEmptyLines();
        }
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateNewLine();
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            PointToMousePos();
            DrawingAfterStart();
        }
        else
        {
            currentLineRenderer = null;
            currentLine = null;
        }
    }

    void CreateNewLine()
    {
        currentLine = new GameObject("Line");
        linesCount++;
        lines.Add(currentLine);
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brushPrefab, currentLine.transform);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos + new Vector2(0.0001f, 0.0001f));
    }

    void AddAPoint(Vector2 pointPos)
    {
        if(currentLineRenderer != null)
        {
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount - 1;
            currentLineRenderer.SetPosition(positionIndex, pointPos);

            if (currentLineRenderer.positionCount > 5)
            {
                ResetBrush();
            }
        }
    }

    void PointToMousePos()
    {
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }

    public void UndoLine()
    {
        linesCount--;
        Destroy(lines[linesCount]);
        lines.Remove(lines[linesCount]);
    }

    public void ResetBrush()
    {
        if(currentLineRenderer != null && Time.timeScale != 0)
        {
            CreateBrush();
        }
    }

    void DrawingAfterStart()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            if (GetComponent<GameManager>().gameStarted)
            {
                GetComponent<GameManager>().noMoreLines = false;
            }
        }
    }

    void DestroyEmptyLines()
    {
        List<GameObject> toDelete = new List<GameObject>();

        foreach(GameObject line in lines)
        {
            if(line.transform.childCount == 0)
            {
                linesCount--;
                toDelete.Add(line);
            }
        }

        foreach(GameObject line in toDelete)
        {
            lines.Remove(line);
            Destroy(line);
        }
    }
}
