using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<GameObject> Levels;

    public GameObject tank;
    public GameObject resetPoint;
    public GameObject greenEnvironment;
    public GameObject desertEnvironment;
    public GameObject snownvironment;

    public int levelnumber = 0;

    public GameObject bgMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        loadLevel(GameManager.instance.numberOfLevel);

        if (!GameManager.instance.bgMusic)
        {
            bgMusic.SetActive(false);
        }

    }

    public void loadLevel(int x)
    {
        levelnumber = PlayFabManager.Instance.sv.lvlhandler;
        Levels[x].gameObject.SetActive(true);

        //tank.GetComponent<TankAI>().buildingIndex = 0;


        //tank.GetComponent<TankAI>().buildings.Clear();

        //for (int i =0;i< Levels[x].GetComponent<LevelInfo>().objectives.Count; i++)
        //{
        //    tank.GetComponent<TankAI>().buildings.Add(Levels[x].GetComponent<LevelInfo>().objectives[i].gameObject);
        //}

        //tank.GetComponent<TankAI>().waypoints.Clear();

        //for (int i = 0; i < Levels[x].GetComponent<LevelInfo>().ways[0].waypoints.Count; i++)
        //{
        //   // tank.GetComponent<TankAI>().waypoints.Add(Levels[x].GetComponent<LevelInfo>().waypoints[i].gameObject.transform);
        //    tank.GetComponent<TankAI>().waypoints.Add(Levels[x].GetComponent<LevelInfo>().ways[0].waypoints[i].gameObject.transform);
        //}

        //tank.GetComponent<TankAI>().numberOfbuildings = 1;
        //tank.GetComponent<TankAI>().building = Levels[x].GetComponent<LevelInfo>().objectives[0].gameObject;
        //tank.GetComponent<TankAI>().nextHouseIndex = 1;
        //tank.GetComponent<TankAI>().wayPointIndex = 0;
        //tank.GetComponent<TankAI>().levelComplete = false;

        //tank.GetComponent<TankAI>().firstHouse = Levels[x].GetComponent<LevelInfo>().ways[0].initialObjective;
        //tank.GetComponent<TankAI>().nextHouseIndex = Levels[x].GetComponent<LevelInfo>().ways[0].nextObjectiveIndex;

        //tank.GetComponent<TankAI>().turretTimeLimit = Levels[x].GetComponent<LevelInfo>().ways[0].turretRotationLimit;
        //tank.GetComponent<TankAI>().thirdHouse = Levels[x].GetComponent<LevelInfo>().ways[0].thirdObjective;

        if (levelnumber >= 15)
        {
            levelnumber = 0;
        }

        if (levelnumber < 5)
        {
            greenEnvironment.SetActive(true);
        }
        else if (levelnumber < 10)
        {
            desertEnvironment.SetActive(true);
        }
        else if (levelnumber < 15)
        {
            snownvironment.SetActive(true);
        }

        levelnumber++;

       





        tank.GetComponent<PlayerAIController>().buildingIndex = 0;


        tank.GetComponent<PlayerAIController>().buildings.Clear();

        for (int i = 0; i < Levels[x].GetComponent<LevelInfo>().objectives.Count; i++)
        {
            tank.GetComponent<PlayerAIController>().buildings.Add(Levels[x].GetComponent<LevelInfo>().objectives[i].gameObject);
        }

        tank.GetComponent<PlayerAIController>().waypoints.Clear();

        for (int i = 0; i < Levels[x].GetComponent<LevelInfo>().ways[0].waypoints.Count; i++)
        {
            // tank.GetComponent<TankAI>().waypoints.Add(Levels[x].GetComponent<LevelInfo>().waypoints[i].gameObject.transform);
            tank.GetComponent<PlayerAIController>().waypoints.Add(Levels[x].GetComponent<LevelInfo>().ways[0].waypoints[i].gameObject.transform);
        }

        tank.GetComponent<PlayerAIController>().numberOfbuildings = 1;
        tank.GetComponent<PlayerAIController>().building = Levels[x].GetComponent<LevelInfo>().objectives[0].gameObject;
        tank.GetComponent<PlayerAIController>().nextHouseIndex = 1;
        tank.GetComponent<PlayerAIController>().wayPointIndex = 0;
        tank.GetComponent<PlayerAIController>().levelComplete = false;

        tank.GetComponent<PlayerAIController>().firstHouse = Levels[x].GetComponent<LevelInfo>().ways[0].initialObjective;
        tank.GetComponent<PlayerAIController>().nextHouseIndex = Levels[x].GetComponent<LevelInfo>().ways[0].nextObjectiveIndex;

        tank.GetComponent<PlayerAIController>().turretTimeLimit = Levels[x].GetComponent<LevelInfo>().ways[0].turretRotationLimit;
        tank.GetComponent<PlayerAIController>().thirdHouse = Levels[x].GetComponent<LevelInfo>().ways[0].thirdObjective;

        UIManager.Instance.levelCompletePanel.SetActive(false);
    }
}
