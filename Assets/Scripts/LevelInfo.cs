using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public List<GameObject> objectives;
    public List<GameObject> collectables;
    public List<WayPoints> ways;
    public int maxTank;
    
}

[Serializable]
public class WayPoints
{
    public List<GameObject> waypoints;
    public int initialObjective;
    public int nextObjectiveIndex;
    public int thirdObjective;
    public int fourthObjective;
    public float turretRotationLimit;
}