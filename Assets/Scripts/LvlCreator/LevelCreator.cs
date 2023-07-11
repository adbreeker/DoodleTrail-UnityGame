using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public GameObject startingGround;
    public GameObject finishGround;

    public List<GameObject> obstaclesPrefabs = new List<GameObject>();

    public Transform obstalceHolder;

    [Space(30f)]
    public bool createLvlScript;
    [Header("Output:"), TextArea(1,40)]
    public string command = "";

    
    public void CreateLevelScript()
    {
        string lvlMethod = "";
        lvlMethod += "Level CreateLvL()\n" +
            "{\n" +
            "   PlayerFinish player = new PlayerFinish(new Vector2(" + ConvertToFloatString(startingGround.transform.position.x) + ", " + ConvertToFloatString(startingGround.transform.position.y) + "), ";
        if(startingGround.transform.rotation.eulerAngles == Vector3.zero)
        {
            lvlMethod += "Quaternion.identity);\n";
        }
        else
        {
            lvlMethod += "Quaternion.Euler(0,0," + ConvertToFloatString(startingGround.transform.rotation.eulerAngles.z) + ");\n";
        }

        lvlMethod += "   PlayerFinish finish = new PlayerFinish(new Vector2(" + ConvertToFloatString(finishGround.transform.position.x) + ", " + ConvertToFloatString(finishGround.transform.position.y) + "), ";
        if (finishGround.transform.rotation.eulerAngles == Vector3.zero)
        {
            lvlMethod += "Quaternion.identity);\n\n";
        }
        else
        {
            lvlMethod += "Quaternion.Euler(0,0," + ConvertToFloatString(finishGround.transform.rotation.eulerAngles.z) + ");\n\n";
        }

        int[] obstaclesTypes = Enumerable.Repeat(0, obstaclesPrefabs.Count).ToArray();
        List<string> obstacleNames = new List<string>();

        foreach (Transform lvlMember in obstalceHolder)
        {
            int obstacleTypeIndex = 0;
            foreach(GameObject obstaclePrefab in obstaclesPrefabs)
            {
                if(PrefabUtility.GetCorrespondingObjectFromSource(lvlMember.gameObject) == obstaclePrefab)
                {
                    obstaclesTypes[obstacleTypeIndex]++;
                    obstacleNames.Add(obstaclePrefab.name + obstaclesTypes[obstacleTypeIndex].ToString());
                    lvlMethod += "  Obstacle " + obstacleNames[obstacleNames.Count - 1] + " = new Obstacle(new Vector2(" + ConvertToFloatString(lvlMember.transform.position.x) + ", " + ConvertToFloatString(lvlMember.transform.position.y) + "), "; 

                    if(lvlMember.rotation.eulerAngles == Vector3.zero)
                    {
                        lvlMethod += "Quaternion.identity, " + obstacleTypeIndex + ");\n";
                    }
                    else
                    {
                        lvlMethod += "Quaternion.Euler(0,0," +  ConvertToFloatString(lvlMember.rotation.eulerAngles.z) + ") ," + obstacleTypeIndex + ");\n";
                    }

                    break;
                }
                obstacleTypeIndex++;
            }
        }

        lvlMethod += "\n    Obstacle[] obstacles = { ";

        for(int i = 0; i<=obstacleNames.Count - 1; i++)
        {
            if(i == obstacleNames.Count - 1)
            {
                lvlMethod += obstacleNames[i] + " };\n\n";
            }
            else
            {
                lvlMethod += obstacleNames[i] + ", ";
            }
        }

        lvlMethod += "  Level lvl = new Level(player, finish, obstacles); \n\n"
            + " return lvl; \n"
            + "}";

        command = lvlMethod;
    }

    string ConvertToFloatString(float value)
    {
        string converted = value.ToString("F2");
        converted = Regex.Replace(converted, @"\s", "").Replace(",", ".");
        converted += "f";
        return converted;
    }

    private void OnValidate()
    {
        CreateLevelScript();
    }

}


