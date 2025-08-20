using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstaclesList", menuName = "ScriptableObjects/ObstaclesList")]
public class ObstaclesListSO : ScriptableObject
{
    public List<GameObject> obstacles;
}
