using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Drawer : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;
    LineRenderer currentLineRenderer;
    Vector2 lastPos;

    EventSystem eventSystem;
    public int linesCount = 0;
    public bool drawPermision = true;
    List<GameObject> lines = new List<GameObject>();


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
        }
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
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
        }
    }

    void CreateBrush()
    {
        
        GameObject brushInstance = Instantiate(brush);
        linesCount++;
        lines.Add(brushInstance);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        //because you gotta have 2 points to start a line renderer, 
        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
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
}
