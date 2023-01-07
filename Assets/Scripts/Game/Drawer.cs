using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drawer : MonoBehaviour
{
    public GameObject linePointPrefab;
    public List<List<GameObject>> lines = new List<List<GameObject>>();
    public List<GameObject> lastLine = new List<GameObject>();
    public int line_index = 0;
    bool nextline = false;

    public GameManager gameManager;

    EventSystem eventSystem;


    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if(!eventSystem.IsPointerOverGameObject() && !IsPointerOverUIObject())
        {
            if (Input.GetMouseButton(0))
            {
                if (PointExist(Camera.current.ScreenToWorldPoint(Input.mousePosition)) == false)
                {
                    Vector3 position = Camera.current.ScreenToWorldPoint(Input.mousePosition);
                    position.z = 0;
                    lastLine.Add(Instantiate(linePointPrefab, position, Quaternion.identity));
                    nextline = true;
                }

                if(gameManager.gameStarted)
                {
                    gameManager.noMoreLines = false;
                }
            }
            else
            {
                if (nextline)
                {
                    lines.Add(new List<GameObject>(lastLine));
                    lastLine.Clear();
                    nextline = false;
                    line_index++;
                }
            }
        }
       

    }

    bool PointExist(Vector3 mousePos)
    {
        Vector3 loc = mousePos;
        loc.z = 0;

        foreach(GameObject point in lastLine)
        {
            if (point.transform.position == loc)
            {
                return true;
            }
        }

        foreach(List<GameObject> line in lines)
        {
            foreach(GameObject point in line)
            {
                if(point.transform.position == loc)
                {
                    return true;
                }    
            }
        }
        return false;
    }

    public void UndoLine()
    {
        if(line_index > 0)
        {
            foreach(GameObject point in lines[line_index-1])
            {
                Destroy(point);
            }
            lines.Remove(lines[line_index - 1]);
            line_index--;
        }
    }


}
