using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Drawer : MonoBehaviour
{

    public Camera m_camera;

    [Header("Brush settings:")]
    public GameObject brushPrefab;
    public Color brushColor = new Color(0.233f, 0.484f, 0.736f, 1.000f);
    public float brushWidth = 0.3f;

    LineRenderer currentLineRenderer;
    Vector2 lastPos;

    [Header("Current status:")]
    EventSystem eventSystem;
    public int linesCount = 0;
    public bool drawPermision = true;

    List<GameObject> lines = new List<GameObject>();
    GameObject currentLine;

    AudioSourceController _drawingSound;


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
        eventSystem = FindFirstObjectByType<EventSystem>();
        LineRenderer brushLineRenderer = brushPrefab.GetComponent<LineRenderer>();
        brushLineRenderer.startColor = brushColor;
        brushLineRenderer.endColor = brushColor;
        brushLineRenderer.startWidth = brushWidth;
        brushLineRenderer.endWidth = brushWidth;
    }

    void Update()
    {
        if(drawPermision && Time.timeScale != 0)
        {
            Drawing();
            DestroyEmptyLines();
        }
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !IsPointerOverUIObject())
        {
            CreateNewLine();
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0) && currentLineRenderer != null)
        {
            PointToMousePos();
            DrawingAfterStart();
            if(_drawingSound == null)
            {
                _drawingSound = SoundManager.Instance.PlaySound(SoundEnum.EFFECT_WRITE, true);
            }
        }
        else
        {
            currentLineRenderer = null;
            currentLine = null;
            if (_drawingSound != null)
            {
                SoundManager.Instance.DestroyAudioSource(_drawingSound);
                _drawingSound = null;
            }
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
