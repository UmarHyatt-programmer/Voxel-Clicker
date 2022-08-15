using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public GameObject tutorial, tapPanel, noInternetPnl, comingSoonPanel;
    public bool isTutorial;
    public GameObject loadingPnl;
    public static UIManager Instance;

    public GameObject levelCompletePanel;
    public bool levelCompleted;

    public Text coinText;

    int levelIndex = 0;

    public GameObject levelPanel;

    public GameObject pausePanel;

    public int earnings = 10;
    public int speedup = 10;

    public int levelNumber;

    public int numberofTank = 1;

    public Button TankButton;

    public int tankWayPointIndex = 1;

    public Text levelText;

    public int previousLevelPlayer = 1;

    public Button speedUpButton;
    public Button earningButton;

    public Slider healthBar;

    public GameObject mainSoldier;

    public Text finalcoins;
    public Text endLevel;

    public GameObject particleEffects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    public GameObject curentLevel;
    private async void Start()
    {
        StartCoroutine("CheckDataConnection");
        for (int i = 0; i < LevelManager.instance.Levels.Count; i++)
        {
            if (LevelManager.instance.Levels[i].gameObject.activeInHierarchy)
            {
                curentLevel = LevelManager.instance.Levels[i].gameObject;
            }
        }

        levelNumber = GameManager.instance.numberOfLevel;
        numberofTank = PlayFabManager.Instance.sv.numberofTanks;

        if (curentLevel.GetComponent<LevelInfo>().maxTank == numberofTank)
        {
            TankButton.interactable = false;
            TankButton.gameObject.transform.GetChild(1).GetComponent<Text>().text = "MAX";
        }
        else if (curentLevel.GetComponent<LevelInfo>().maxTank < numberofTank)
        {
            TankButton.interactable = false;
            TankButton.gameObject.transform.GetChild(1).GetComponent<Text>().text = "MAX";
        }

        earnings = PlayFabManager.Instance.sv.earningCost;
        speedup = PlayFabManager.Instance.sv.speedupCost;
        levelText.text = "Lv." + (levelNumber + 1);

        if (DataHandler.instance.coins > 1000)
        {
            float tcoins = DataHandler.instance.coins / 1000;
            coinText.text = "" + tcoins.ToString("F2") + "K";
        }
        else
        {
            coinText.text = "" + DataHandler.instance.coins.ToString();
        }

        if (earnings >= 1000)
        {
            tempEarning = earnings / 1000;
            DataHandler.instance.earningText.text = tempEarning.ToString("F2") + "K" + " COINS";
        }
        else
        {
            DataHandler.instance.earningText.text = earnings + " COINS";
        }

        if (speedup > 1000)
        {
            tempSpeed = speedup / 1000;
            DataHandler.instance.speedupText.text = tempSpeed.ToString("F2") + "K" + " COINS";
        }
        else
        {
            DataHandler.instance.speedupText.text = speedup + " COINS";
        }


        if (DataHandler.instance.coins < earnings)
        {
            earningButton.interactable = false;
        }

        if (DataHandler.instance.coins < speedup)
        {
            speedUpButton.interactable = false;
        }
        if (DataHandler.instance.coins < 10)
        {
            TankButton.interactable = false;
        }
        int myTank = numberofTank - 1;
        for (int i = 0; i < myTank; i++)
        {
            //            print("main for loop ");
            if (i < curentLevel.GetComponent<LevelInfo>().maxTank - 1)
            {
                await Task.Delay(500);
                TankUpgrade();
            }
            //            else { Debug.Log("hiiiiiiiii"); };
        }

        player = tank.GetComponent<PlayerAIController>();

    }
    IEnumerator CheckDataConnection()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            // print("level number is "+PlayFabManager.Instance.sv.numberOfLevel);
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                noInternetPnl.SetActive(true);
                //        Debug.Log("Error. Check internet connection!");
                Time.timeScale = 0;
            }
            else if(noInternetPnl.activeInHierarchy)
            {
                noInternetPnl.SetActive(false);
                Time.timeScale = 1;
                Debug.Log("you have internet connection!");
            }
        }
    }
    PlayerAIController player;
    float coins;

    public void nextLevel()
    {
        // DOTween.KillAll();
        loadingPnl.SetActive(true);
        LoadGame();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadGame()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    float tempEarning;
    public void EarningsUpgrade()
    {
        if (DataHandler.instance.coins >= earnings)
        {
            DataHandler.instance.coins -= earnings;
            DataHandler.instance.earnings += 5;
            earnings *= 2;
            if (earnings >= 1000)
            {
                tempEarning = earnings / 1000;
                DataHandler.instance.earningText.text = tempEarning.ToString("F2") + "K" + " COINS";
            }
            else
            {
                DataHandler.instance.earningText.text = earnings + " COINS";
            }

            if (DataHandler.instance.coins > 1000)
            {
                float tcoins = DataHandler.instance.coins / 1000;
                coinText.text = "" + tcoins.ToString("F2") + "K";
            }
            else
            {
                coinText.text = "" + DataHandler.instance.coins.ToString();
            }

            if (DataHandler.instance.coins < earnings)
            {
                earningButton.interactable = false;
            }

            if (DataHandler.instance.coins < speedup)
            {
                speedUpButton.interactable = false;
            }

        }
    }

    public void TankCoinUpdate()
    {
        if (DataHandler.instance.coins >= earnings)
        {
            DataHandler.instance.coins -= earnings;
            DataHandler.instance.earnings += 5;
            earnings *= 2;
            if (earnings >= 1000)
            {
                tempEarning = earnings / 1000;
                DataHandler.instance.earningText.text = tempEarning.ToString("F2") + "K" + " COINS";
            }
            else
            {
                DataHandler.instance.earningText.text = earnings + " COINS";
            }

            if (DataHandler.instance.coins > 1000)
            {
                float tcoins = DataHandler.instance.coins / 1000;
                coinText.text = "" + tcoins.ToString("F2") + "K";
            }
            else
            {
                coinText.text = "" + DataHandler.instance.coins.ToString();
            }

            if (DataHandler.instance.coins < earnings)
            {
                TankButton.interactable = false;
            }

            if (DataHandler.instance.coins < speedup)
            {
                TankButton.interactable = false;
            }

        }
    }
    float tempSpeed;
    public void SpeedUpgrade()
    {
        if (DataHandler.instance.coins >= speedup)
        {
            DataHandler.instance.coins -= speedup;

            for (int i = 0; i < DataHandler.instance.Tanks.Count; i++)
            {
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().normalSpeed += DataHandler.instance.speedUp;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().speed += DataHandler.instance.speedUp;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().maxSpeed += DataHandler.instance.speedUp;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().time = 0;
            }

            speedup *= 2;

            if (speedup > 1000)
            {
                tempSpeed = speedup / 1000;
                DataHandler.instance.speedupText.text = tempSpeed.ToString("F2") + "K" + " COINS";
            }
            else
            {
                DataHandler.instance.speedupText.text = speedup + " COINS";
            }

            if (DataHandler.instance.coins > 1000)
            {
                float tcoins = DataHandler.instance.coins / 1000;
                coinText.text = "" + tcoins.ToString("F2") + "K";
            }
            else
            {
                coinText.text = "" + DataHandler.instance.coins.ToString();
            }


            if (DataHandler.instance.coins < speedup)
            {
                speedUpButton.interactable = false;
            }

            if (DataHandler.instance.coins < earnings)
            {
                earningButton.interactable = false;
            }

        }
    }

    public GameObject tank;
    public GameObject[] tankSpawnPosition;
    public int spawnpos = 0;

    int buildingIndexIncrement = 1;
    public void TankUpgrade()
    {
        GameObject tnk = Instantiate(tank, tankSpawnPosition[spawnpos].transform.position, tankSpawnPosition[spawnpos].transform.rotation);
        PlayerAIController ai = tnk.GetComponent<PlayerAIController>();

        if (spawnpos == 0)
        {
            for (int i = 0; i < LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().ways[tankWayPointIndex].waypoints.Count; i++)
            {
                //  Debug.Log("first for loop call");
                //ai.waypoints.Add(LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().waypoints[i].transform);
                ai.waypoints.Add(LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().ways[tankWayPointIndex].waypoints[i].gameObject.transform);
            }

            for (int j = 0; j < LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().objectives.Count; j++)
            {
                //  Debug.Log("second for loop call");
                ai.buildings.Add(LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().objectives[j].gameObject);
            }

            ai.building = ai.buildings[0];
            ai.firstHouse = LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().ways[tankWayPointIndex].initialObjective;
            ai.nextHouseIndex = LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().ways[tankWayPointIndex].nextObjectiveIndex;
            ai.thirdHouse = LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().ways[tankWayPointIndex].thirdObjective;
            ai.turretTimeLimit = LevelManager.instance.Levels[levelNumber].GetComponent<LevelInfo>().ways[tankWayPointIndex].turretRotationLimit;
            ai.speed = PlayFabManager.Instance.sv.tankSpeed;
            ai.maxSpeed = PlayFabManager.Instance.sv.maxSpeed;

        }

        tankWayPointIndex++;

        for (int i = 0; i < DataHandler.instance.Tanks.Count; i++)
        {
            // print("targetdetected goes to false ");
            DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().stopMoving = false;
            DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().targetDetected = false;
            DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().lookAtBuilding = false;

            if (DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().wayPointIndex == 0)
            {
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().firstHouse += buildingIndexIncrement;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().nextHouseIndex += buildingIndexIncrement;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().thirdHouse += buildingIndexIncrement;

                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build1 = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().firstHouse;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build2 = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().nextHouseIndex;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build3 = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().thirdHouse;

            }
            else
            {
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().firstHouse = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build1 + buildingIndexIncrement;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().nextHouseIndex = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build2 + buildingIndexIncrement;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().thirdHouse = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build3 + buildingIndexIncrement;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build1 = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().firstHouse;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build2 = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().nextHouseIndex;
                DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().build3 = DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().thirdHouse;

            }

            DataHandler.instance.Tanks[i].transform.LookAt(DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().waypoints[DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().wayPointIndex]);

        }


        DataHandler.instance.Tanks.Add(tnk);
    }

    public void TankIncrement()
    {
        numberofTank++;
        previousLevelPlayer = numberofTank;
        if (numberofTank >= curentLevel.GetComponent<LevelInfo>().maxTank)
        {
            TankButton.interactable = false;
            TankButton.gameObject.transform.GetChild(1).GetComponent<Text>().text = "MAX";
        }
    }

    public GameObject rocket;
    public List<GameObject> rocketPoints;
    public Button powerUp;
    public GameObject refrence;

    int bombSpawnIndex = 1;

    public GameObject powerUpInstantiate;
    public void planeAttack()
    {
        for (int i = 0; i < 3; i++)
        {

            GameObject obj = Instantiate(rocket, powerUpInstantiate.transform.position, refrence.transform.rotation);
            obj.transform.rotation = Quaternion.Euler(180, 0, 0);
            obj.transform.LookAt(refrence.transform.GetChild(1));

            if (i == 1)
            {
                obj.transform.position += new Vector3(10, 0, 0);
                obj.GetComponent<Bomb>().effect = true;
            }
            else if (i == 2)
            {
                obj.transform.position += new Vector3(0, 0, 10);

            }
            else if (i == 3)
            {
                obj.transform.position += new Vector3(10, 0, 10);
            }

        }
        //powerUp.interactable = false;
        //powerUp.gameObject.SetActive(false);

    }

    public Gradient gradient;
    public Image fillImage;

    public void buildingHealthBarHandler(int health)
    {
        healthBar.maxValue = health;
    }
    public void BuildingHealthValueSetter(int health)
    {
        healthBar.value = health;
        fillImage.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void ResetProgress()
    {
        //SaveData.Instance = new SaveData();
        GameManager.instance.numberOfLevel = 0;
        DataHandler.instance.coins = 100;
        PlayFabManager.Instance.sv.numberOfLevel = GameManager.instance.numberOfLevel;
        PlayFabManager.Instance.sv.totalCoins = DataHandler.instance.coins;
        PlayFabManager.Instance.sv.numberofTanks = 1;
        PlayFabManager.Instance.sv.earningCost = 10;
        PlayFabManager.Instance.sv.speedupCost = 10;
        PlayFabManager.Instance.sv.lvlhandler = 0;
        PlayFabManager.Instance.sv.sfx = true;
        PlayFabManager.Instance.sv.bgMusic = true;
        PlayFabManager.Instance.sv.vibration = true;
        PlayFabManager.Instance.setPlayerData();
        //*    GF_SaveLoad.SaveProgress();
        loadingPnl.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // SceneManager.LoadScene("Loading");
    }
    public void LogOut()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("logout Apple delete keys");
        logout = false;
        AppleUserLogin.instance.LogOutApple();
        PlayFabManager.Instance.LogoutPlayFab();
        StartCoroutine(OnLogOut());
    }
    public bool logout = false;
    IEnumerator OnLogOut()
    {
        Debug.Log("Before Logout true");
        yield return new WaitUntil(() => logout == true);
        Debug.Log("After Logout true");
        SceneManager.LoadScene(0);
    }

    public void pause()
    {
                                                                               MopubMediation.Instance.ShowInterstitial();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        for (int i = 0; i < DataHandler.instance.Tanks.Count; i++)
        {
            DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().controll = false;
        }
    }
    public void back()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        for (int i = 0; i < DataHandler.instance.Tanks.Count; i++)
        {
            DataHandler.instance.Tanks[i].GetComponent<PlayerAIController>().controll = true;
        }
    }
    private void Update()
    {
        Debug.Log("TimeScale is "+ Time.timeScale);
    }
}
