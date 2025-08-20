using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    public GameObject startingGround;
    public GameObject finishGround;
    public GameObject baseStar;

    public ObstaclesListSO obstaclesListSO;

    public Transform obstacleHolder;

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
            lvlMethod += "Quaternion.Euler(0,0," + ConvertToFloatString(startingGround.transform.rotation.eulerAngles.z) + "));\n";
        }

        lvlMethod += "   PlayerFinish finish = new PlayerFinish(new Vector2(" + ConvertToFloatString(finishGround.transform.position.x) + ", " + ConvertToFloatString(finishGround.transform.position.y) + "), ";
        if (finishGround.transform.rotation.eulerAngles == Vector3.zero)
        {
            lvlMethod += "Quaternion.identity);\n\n";
        }
        else
        {
            lvlMethod += "Quaternion.Euler(0,0," + ConvertToFloatString(finishGround.transform.rotation.eulerAngles.z) + "));\n\n";
        }

        int[] obstaclesTypes = Enumerable.Repeat(0, obstaclesListSO.obstacles.Count).ToArray();
        List<string> obstacleNames = new List<string>();

        foreach (Transform lvlMember in obstacleHolder)
        {
            int obstacleTypeIndex = 0;
            foreach(GameObject obstaclePrefab in obstaclesListSO.obstacles)
            {
                if(lvlMember.GetComponent<Obstacle>().obstacleName == obstaclePrefab.GetComponent<Obstacle>().obstacleName || lvlMember.gameObject == baseStar)
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
                        lvlMethod += "Quaternion.Euler(" + ConvertToFloatString(lvlMember.rotation.eulerAngles.x) + ", "
                            + ConvertToFloatString(lvlMember.rotation.eulerAngles.y) + ", " 
                            + ConvertToFloatString(lvlMember.rotation.eulerAngles.z) + ") ," + obstacleTypeIndex + ");\n";
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
                lvlMethod += obstacleNames[i];
            }
            else
            {
                lvlMethod += obstacleNames[i] + ", ";
            }
        }
        lvlMethod += " };\n\n";

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

}

#if UNITY_EDITOR
[CustomEditor(typeof(LevelCreator))]
class LevelCreatorEditor : Editor
{
    //create or use existing
    public bool create = true;
    public int selectedLvl = 0;

    public override void OnInspectorGUI()
    {
        LevelCreator script = (LevelCreator)target;

        GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
        labelStyle.fontSize = 20;
        labelStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("startingGround"), new GUIContent("Starting Ground"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("finishGround"), new GUIContent("Finish Ground"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("baseStar"), new GUIContent("Base Star"), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("obstaclesListSO"), new GUIContent("Obstacle Prefabs"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("obstacleHolder"), new GUIContent("Obstacle Holder"), true);

        GUILayout.Space(20f);

        if(GUILayout.Button(create? "Use Existing" : "Create"))
        {
            create = !create;
            script.command = "";
        }

        GUILayout.Space(20f);

        if(create)
        {
            EditorGUILayout.LabelField("Level Command:", labelStyle);
            GUILayout.Space(8f);
            SerializedProperty levelCommandProperty = serializedObject.FindProperty("command");
            levelCommandProperty.stringValue = EditorGUILayout.TextArea(levelCommandProperty.stringValue);

            GUILayout.Space(10f);

            if (GUILayout.Button("Create level script", GUILayout.Height(40f)))
            {
                script.CreateLevelScript();
            }

            GUILayout.Space(10f);

            if (GUILayout.Button("Copy to clipboard", GUILayout.Height(40f)))
            {
                GUIUtility.systemCopyBuffer = script.command;
            }

            GUILayout.Space(10f);

            if (GUILayout.Button("Reset", GUILayout.Height(40f)))
            {
                script.command = "";
                ClearLevel(script);
            }
        }
        else
        {
            GUILayout.Space(20f);

            Levels levelsList = new Levels();
            string[] levelsToSelect = new string[levelsList.LevelsCount()];
            for(int i = 0; i<levelsList.LevelsCount(); i++)
            {
                levelsToSelect[i] = "Level " + i;
            }

            selectedLvl = EditorGUILayout.Popup("Level to spawn:", selectedLvl, levelsToSelect);

            GUILayout.Space(10f);

            if (GUILayout.Button("Spawn", GUILayout.Height(40f)))
            {
                SetLevel(selectedLvl, script);
            }
        }

       

        serializedObject.ApplyModifiedProperties();
    }

    void SetLevel(int lvlIndex, LevelCreator script)
    {
        ClearLevel(script);

        Levels.Level level = new Levels().getLevel(lvlIndex);

        script.startingGround.transform.position = level.player.position;
        script.startingGround.transform.rotation = level.player.rotation;

        script.finishGround.transform.position = level.finish.position;
        script.finishGround.transform.rotation = level.finish.rotation;

        script.baseStar.transform.position = level.obstacles[0].position;
        script.baseStar.transform.rotation = level.obstacles[0].rotation;

        for(int i = 1; i <level.obstacles.Length; i++)
        {
            Instantiate(script.obstaclesListSO.obstacles[level.obstacles[i].type], level.obstacles[i].position, level.obstacles[i].rotation, script.obstacleHolder);
        }
    }

    void ClearLevel(LevelCreator script)
    {
        script.startingGround.transform.position = new Vector3(-4, 0, 0);
        script.startingGround.transform.rotation = Quaternion.Euler(0, 0, 0);

        script.finishGround.transform.position = new Vector3(4, 0, 0);
        script.finishGround.transform.rotation = Quaternion.Euler(0, 0, 0);

        script.baseStar.transform.position = new Vector3(0, 1, 0);
        script.baseStar.transform.rotation = Quaternion.Euler(0, 0, 0);

        while(script.obstacleHolder.childCount != 1)
        {
            foreach (Transform obstacle in script.obstacleHolder)
            {
                if (obstacle.gameObject != script.baseStar)
                {
                    DestroyImmediate(obstacle.gameObject);
                }
            }
        }
    }
}
#endif

