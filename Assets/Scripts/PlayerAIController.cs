using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAIController : MonoBehaviour
{
    bool dataUpdate;
    public List<Transform> waypoints;
    public int wayPointIndex;

    public List<GameObject> buildings;
    public int buildingIndex;

    public int speed;

    [HideInInspector]
    public int normalSpeed;

    float distance;

    [HideInInspector]
    public bool stopMoving;

    public GameObject building;
    private float lastTimeShoot = 0f;
    public float timebetweenShoots = 1.5f;
    public GameObject turret = null;
    public GameObject turretPoint;
    public GameObject rocket = null;
    public GameObject rocketInstantiatePoint = null;
    public float bodyRotationSpeed = 30f;
    public int maxSpeed;

    [HideInInspector]
    public float time = 0;
    float timetoRotate = 0;
    float rotSpeed;

    bool tapping;
    public bool targetDetected;
    public bool lookAtBuilding;

    public bool levelComplete;

    RaycastHit hit;

    public GameObject raycastPoint;

    bool test;

    float rotate = 17f;

    int buildingCheck = 0;

    bool isComplete;

    public bool isMainTank;

    Animator animator;

    public int build1, build2, build3, builIndex;

    bool isLooking;

    public List<GameObject> rocketList;

    GameObject rocketContainer;

    bool settingHealthBar = true;

    public bool controll = true;
    async void Start()
    {
        //  Application.targetFrameRate=300;
        //  UIManager.Instance.TankButton.interactable = false;
        //        print("save data totalearn level  "+ SaveData.Instance.totalEarnLevel); 
        dataUpdate = true;
        animator = this.gameObject.GetComponent<Animator>();
        if (GameManager.instance.numberOfLevel == 0)
        {
            TankButtonOn(false);
            normalSpeed = speed;
            // normalSpeed = SaveData.Instance.normalspeed;
            maxSpeed = speed + 15;

        }
        else
        {
            if (isMainTank)
            {
                //                Debug.Log("getting data from save data for the soldier");
                normalSpeed = PlayFabManager.Instance.sv.normalspeed;
                speed = PlayFabManager.Instance.sv.tankSpeed;
                maxSpeed = PlayFabManager.Instance.sv.maxSpeed;
                UIManager.Instance.earnings = PlayFabManager.Instance.sv.earningCost;
                UIManager.Instance.speedup = PlayFabManager.Instance.sv.speedupCost;
                UIManager.Instance.coinText.text = "" + (PlayFabManager.Instance.sv.totalCoins / 1000).ToString("F2") + "K";
                UIManager.Instance.previousLevelPlayer = PlayFabManager.Instance.sv.lastLevelPlayers;
                DataHandler.instance.earnings = PlayFabManager.Instance.sv.totalEarnLevel;
                DataHandler.instance.speedUp = PlayFabManager.Instance.sv.totalSpeedLevel;
                DataHandler.instance.coins = PlayFabManager.Instance.sv.totalCoins;
                UIManager.Instance.buildingHealthBarHandler(building.gameObject.GetComponent<BuildingHealth>().buildingHealth);
                UIManager.Instance.BuildingHealthValueSetter(building.gameObject.GetComponent<BuildingHealth>().buildingHealth);
            }
        }
        rotSpeed = bodyRotationSpeed;
        wayPointIndex = 0;
        normalSpeed = speed;
        maxSpeed = speed + 15;
        await Task.Delay(1000);
        if (wayPointIndex < waypoints.Count)
        {
            transform.LookAt(waypoints[wayPointIndex].position);
        }
        build1 = firstHouse;
        build2 = nextHouseIndex;
        build3 = thirdHouse;
        //GameObject rocketContainer = new GameObject("RocketContainer");

        //for(int i = 0; i < 60; i++)
        //{
        //    GameObject rock = Instantiate(rocket);
        //    rock.transform.parent = rocketContainer.transform;
        //    rocketList.Add(rock);
        //    rock.SetActive(false);
        //}
        UIManager.Instance.refrence = building;

    }
    public void BuySolder()
    {
        if (LevelManager.instance.levelnumber == 1 && isMainTank)
        {
            Patrol();
        }
    }
    public void TankButtonOn(bool x)
    {
        if (isMainTank && UIManager.Instance.levelNumber == 0)
        {
            //          print("button true ");
            UIManager.Instance.TankButton.interactable = x;
        }
    }
    // Update is called once per frame
    void Update()
    {
        turret.transform.Rotate(Vector3.forward * rotate * Time.deltaTime);

        if (Physics.Raycast(raycastPoint.transform.position, raycastPoint.transform.forward, out hit, 45f))
        {
            if (targetDetected)
            {
                //                   print("target detected");
                if (hit.collider.CompareTag("building"))
                {
                    test = true;
                    if (!building.GetComponent<BuildingHealth>().destroyed)
                    {
                        if (controll)
                        {
                            Shoot();
                        }
                    }
                }
                else if (hit.collider.CompareTag("limit"))
                {
                    if (test)
                    {
                        bodyRotationSpeed = -bodyRotationSpeed;
                        test = false;
                    }
                }
            }
        }

        if (!isComplete)
        {
            Controlls();
        }

        if (targetDetected)
        {
            animator.SetBool("isWalkin", false);
            this.gameObject.transform.Rotate(Vector3.up, this.bodyRotationSpeed * Time.deltaTime);

            if (isMainTank)
            {
                if (building.GetComponent<BuildingHealth>().powerAttack == true)
                {
                    if (building.GetComponent<BuildingHealth>().buildingHealth < 220)
                    {
                        UIManager.Instance.planeAttack();
                        building.GetComponent<BuildingHealth>().powerAttack = false;
                    }
                }
            }

        }

        if (wayPointIndex < waypoints.Count)
        {
            distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].position);
            //print("distance  "+distance);
        }
        ///  Debug.Log("waypoint point ");

        if (distance < 3f)
        {
            //            print("destination");
            if (wayPointIndex == 0)
            {
                if (UIManager.Instance.levelNumber == 0)
                {
                    UIManager.Instance.tutorial.SetActive(true);
                }
                TankButtonOn(true);
            }
            Increment();
        }

        if (!stopMoving && MenuHandler.Instance.isStart)
        {

            Patrol();
            animator.SetBool("isWalkin", true);
        }
        else
        {
            if (lookAtBuilding)
            {
                if (building != null)
                {
                    //u* add condition 
                    if(buildings[buildingIndex].transform.GetChild(3))
                    {
                    transform.LookAt(buildings[buildingIndex].transform.GetChild(3).transform.position);
                    lookAtBuilding = false;
                    }

                }
            }

            if (!building.GetComponent<BuildingHealth>().destroyed)
            {
                // Shoot();
            }
            else
            {
                buildingIndex++;

                transform.LookAt(waypoints[wayPointIndex].position);

                if (building.transform.GetChild(1).transform.GetComponent<VoxelBomb>())
                {
                    building.transform.GetChild(1).transform.GetComponent<VoxelBomb>().triggered = true;
                }

                if (buildings.Count > 2)
                {
                    // turretTimeLimit = .6f;
                }

                stopMoving = false;
                //                 print("targetdetected goes to false ");
                targetDetected = false;
                lookAtBuilding = false;
                buildingCheck++;
                if (buildingCheck < buildings.Count)
                {
                    building = buildings[buildingCheck].gameObject;
                    UIManager.Instance.refrence = building;
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    Destroy(bullets[i]);
                }

                settingHealthBar = true;
            }
        }

        if (settingHealthBar)
        {
            if (isMainTank)
            {
                UIManager.Instance.buildingHealthBarHandler(building.gameObject.GetComponent<BuildingHealth>().buildingHealth);
                UIManager.Instance.BuildingHealthValueSetter(building.gameObject.GetComponent<BuildingHealth>().buildingHealth);
            }
            settingHealthBar = false;
        }

    }

    void Patrol()
    {
        if (wayPointIndex < waypoints.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[wayPointIndex].position, Time.deltaTime * speed);
        }
    }
    public int numberOfbuildings = 1;

    public int nextHouseIndex;
    public int firstHouse;
    public int thirdHouse;

    void Increment()
    {
        wayPointIndex++;
        if (wayPointIndex == firstHouse || wayPointIndex == nextHouseIndex || wayPointIndex == thirdHouse)
        {
            if (building != null)
            {
                stopMoving = true;
                targetDetected = true;
                lookAtBuilding = true;

                numberOfbuildings++;
                firstHouse = nextHouseIndex;
                nextHouseIndex = thirdHouse;
                animator.SetBool("isWalkin", false);
            }
        }

        if (wayPointIndex >= waypoints.Count)
        {
            if (speed == maxSpeed)
            {
                speed = normalSpeed;
            }

            //wayPointIndex = 0;

            levelComplete = true;
            PlayFabManager.Instance.sv.numberOfLevel = GameManager.instance.numberOfLevel;
            isComplete = true;

            if (isMainTank)
            {
                if (dataUpdate)
                {
                    dataUpdate = false;
                    GameManager.instance.numberOfLevel++;

                    PlayFabManager.Instance.sv.numberOfLevel = GameManager.instance.numberOfLevel;
                    PlayFabManager.Instance.sv.normalspeed = normalSpeed;
                    PlayFabManager.Instance.sv.tankSpeed = speed;
                    PlayFabManager.Instance.sv.maxSpeed = maxSpeed;
                    PlayFabManager.Instance.sv.totalCoins = DataHandler.instance.coins;
                    PlayFabManager.Instance.sv.earningCost = UIManager.Instance.earnings;
                    PlayFabManager.Instance.sv.speedupCost = UIManager.Instance.speedup;
                    //                print("data update earinings  "+ DataHandler.instance.earnings); 
                    PlayFabManager.Instance.sv.totalEarnLevel = DataHandler.instance.earnings;
                    PlayFabManager.Instance.sv.totalSpeedLevel = DataHandler.instance.speedUp;
                    PlayFabManager.Instance.sv.spawnPos = UIManager.Instance.spawnpos;
                    PlayFabManager.Instance.sv.numberofTanks = UIManager.Instance.numberofTank;
                    PlayFabManager.Instance.sv.lastLevelPlayers = UIManager.Instance.previousLevelPlayer;
                    PlayFabManager.Instance.sv.sfx = GameManager.instance.sfx;
                    PlayFabManager.Instance.sv.vibration = GameManager.instance.vibration;
                    PlayFabManager.Instance.sv.bgMusic = GameManager.instance.bgMusic;
                    PlayFabManager.Instance.sv.lvlhandler = LevelManager.instance.levelnumber;

                    //*    GF_SaveLoad.SaveProgress();
                    PlayFabManager.Instance.setPlayerData();
                }
            }

            DataHandler.instance.tankLevelCheck();
        }

        if (!levelComplete)
        {
            transform.LookAt(waypoints[wayPointIndex].position);
            animator.SetBool("isWalkin", true);

        }

    }

    List<GameObject> bullets = new List<GameObject>();


    int bulletIndex = 0;
    public void Shoot()
    {
        if ((Time.fixedTime - lastTimeShoot) > this.timebetweenShoots)
        {
            lastTimeShoot = Time.fixedTime;
            GameObject newGameObject = Instantiate(rocket, rocketInstantiatePoint.transform.position, rocketInstantiatePoint.transform.rotation);
            bullets.Add(newGameObject);
            //rocketList[bulletIndex].transform.position = rocketInstantiatePoint.transform.position;
            //rocketList[bulletIndex].transform.rotation = rocketInstantiatePoint.transform.rotation;
            //rocketList[bulletIndex].GetComponent<VoxelBomb>().triggered = false;
            //rocketList[bulletIndex].GetComponent<VoxelBomb>().exploded = false;
            //rocketList[bulletIndex].SetActive(true);
            //bulletIndex++;

            //if(bulletIndex >= rocketList.Count)
            //{
            //    bulletIndex = 0;
            //}

        }
    }

    public float turretTimeLimit;

    float tankRotateLimit;
    void Controlls()
    {
        timetoRotate += Time.deltaTime;
        time += Time.deltaTime;
        tankRotateLimit += Time.deltaTime;

        if (timetoRotate > turretTimeLimit)
        {
            timetoRotate = 0;
            rotate = -rotate;
            //bodyRotationSpeed = -bodyRotationSpeed;
        }

        if (tankRotateLimit > 3)
        {
            bodyRotationSpeed = -bodyRotationSpeed;
            tankRotateLimit = 0;
        }

        if (time > 0.2f)
        {
            speed = 5;
            timebetweenShoots = 1.5f;
        }

        //if(touch.phase==TouchPhase.Began)
        if (MenuHandler.Instance.isStart && (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)))
        {
//            print("tapped");

            if (controll)
            {
                time = 0;
                Time.timeScale = 1f;
                timebetweenShoots = .3f;
                tapping = true;
            }

        }

        if (tapping)
        {
            speed += 15;
            tapping = false;
        }
        else if (!tapping)
        {
            // speed=5;
        }

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        if (bodyRotationSpeed > 30)
        {
            bodyRotationSpeed = 30;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bounds"))
        {
            //if (speed == maxSpeed)
            //{
            //    speed = normalSpeed;
            //}

            //wayPointIndex = 0;

            //SaveData.Instance.numberOfLevel = GameManager.instance.numberOfLevel;
            //isComplete = true;

            //if (isMainTank)
            //{
            //    GameManager.instance.numberOfLevel++;
            //    SaveData.Instance.numberOfLevel = GameManager.instance.numberOfLevel;
            //    SaveData.Instance.normalspeed = normalSpeed;
            //    SaveData.Instance.tankSpeed = speed;
            //    SaveData.Instance.maxSpeed = maxSpeed;
            //    SaveData.Instance.totalCoins = DataHandler.instance.coins;
            //    SaveData.Instance.earningCost = UIManager.Instance.earnings;
            //    SaveData.Instance.speedupCost = UIManager.Instance.speedup;
            //    SaveData.Instance.totalEarnLevel = DataHandler.instance.earnings;
            //    SaveData.Instance.totalSpeedLevel = DataHandler.instance.speedUp;
            //    SaveData.Instance.spawnPos = UIManager.Instance.spawnpos;
            //    SaveData.Instance.numberofTanks = UIManager.Instance.numberofTank;

            //    GF_SaveLoad.SaveProgress();

            //}

            //levelComplete = true;
            //DataHandler.instance.tankLevelCheck();
            //Debug.Log("Level completed called on trigger");
        }
    }
}
