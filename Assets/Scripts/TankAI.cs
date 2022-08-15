using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class TankAI : MonoBehaviour
{
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
    bool targetDetected;
    bool lookAtBuilding;

    public bool levelComplete;

    RaycastHit hit;

    public GameObject raycastPoint;

    bool test;

    float rotate = 17f;

    int buildingCheck = 0;

    bool isComplete;

    public bool isMainTank;

    public GameObject particle1;
    public GameObject particle2;

    async void Start()
    {
        if (GameManager.instance.numberOfLevel == 0)
        {
            normalSpeed = speed;
            maxSpeed = speed + 15;
        }
        else
        {
            if (isMainTank)
            {
                normalSpeed = PlayFabManager.Instance.sv.normalspeed;
                speed = PlayFabManager.Instance.sv.tankSpeed;
                maxSpeed = PlayFabManager.Instance.sv.maxSpeed;
                Debug.Log(UIManager.Instance == null);
                UIManager.Instance.earnings = PlayFabManager.Instance.sv.earningCost;
                UIManager.Instance.speedup = PlayFabManager.Instance.sv.speedupCost;
                UIManager.Instance.coinText.text = "" + DataHandler.instance.coins;
                DataHandler.instance.earnings = PlayFabManager.Instance.sv.totalEarnLevel;
                DataHandler.instance.speedUp = PlayFabManager.Instance.sv.totalSpeedLevel;
                DataHandler.instance.coins = PlayFabManager.Instance.sv.totalCoins;
                Debug.Log("While loading progress from Save data , total number of tanks =  " + UIManager.Instance.numberofTank);
                Debug.Log("While loading progress from Save data , total number of tanks =  " + PlayFabManager.Instance.sv.numberofTanks);
               // Time.timeScale = 0f;
            }
        }
        rotSpeed = bodyRotationSpeed;
        wayPointIndex = 0;
        Debug.Log(waypoints.Count);
        normalSpeed = speed;
        maxSpeed = speed + 15;
        await Task.Delay(1000);
        transform.LookAt(waypoints[wayPointIndex].position);
    }
    public void BuySolder()
    {
        if(LevelManager.instance.levelnumber==1&&isMainTank)
        {
            print("increment done ");
          wayPointIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        turret.transform.Rotate(Vector3.forward * rotate * Time.deltaTime);

        if (Physics.Raycast(raycastPoint.transform.position, raycastPoint.transform.forward, out hit, 45f))
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.CompareTag("building"))
            {
                test = true;
                if (!building.GetComponent<BuildingHealth>().destroyed)
                {
                    Shoot();
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

        if (!isComplete)
        {
            Controlls();
        }

        if (targetDetected)
        {
            this.gameObject.transform.Rotate(Vector3.up, this.bodyRotationSpeed * Time.deltaTime);
        }

        distance = Vector3.Distance(transform.position, waypoints[wayPointIndex].position);

        if (distance < 1f)
        {
            Increment();
        }

        if (!stopMoving)
        {
            Patrol();
        }
        else
        {
            if (lookAtBuilding)
            {
                if (building != null)
                {
                    
                    transform.LookAt(buildings[buildingIndex].transform.GetChild(3).transform.position);
                    buildingIndex++;
                    lookAtBuilding = false;
                }
            }

            if (!building.GetComponent<BuildingHealth>().destroyed)
            {
                // Shoot();
            }
            else
            {
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
                targetDetected = false;
                lookAtBuilding = false;
                buildingCheck++;
                if (buildingCheck < buildings.Count)
                {
                    building = buildings[buildingCheck].gameObject;
                }
            }
        }
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public int numberOfbuildings = 1;

    public int nextHouseIndex;
    public int firstHouse;
    public int thirdHouse;

    void Increment()
    {
        wayPointIndex++;

        if (wayPointIndex == firstHouse || wayPointIndex == nextHouseIndex)
        {
            if (building != null)
            {
                stopMoving = true;
                targetDetected = true;
                lookAtBuilding = true;
                numberOfbuildings++;
                firstHouse = nextHouseIndex;
                nextHouseIndex = thirdHouse;
            }
        }

        if (wayPointIndex >= waypoints.Count)
        {
            if(speed == maxSpeed)
            {
                speed = normalSpeed;
            }

            wayPointIndex = 0;
            levelComplete = true;
            print("number of lavel update in Tank AI "+ GameManager.instance.numberOfLevel);
            PlayFabManager.Instance.sv.numberOfLevel = GameManager.instance.numberOfLevel;
            isComplete = true;

            if (isMainTank)
            {
                GameManager.instance.numberOfLevel++;
                PlayFabManager.Instance.sv.numberOfLevel = GameManager.instance.numberOfLevel;
                PlayFabManager.Instance.sv.normalspeed = normalSpeed;
                PlayFabManager.Instance.sv.tankSpeed = speed;
                PlayFabManager.Instance.sv.maxSpeed = maxSpeed;
                PlayFabManager.Instance.sv.totalCoins = DataHandler.instance.coins;
                PlayFabManager.Instance.sv.earningCost = UIManager.Instance.earnings;
                PlayFabManager.Instance.sv.speedupCost = UIManager.Instance.speedup;
                PlayFabManager.Instance.sv.totalEarnLevel = DataHandler.instance.earnings;
                PlayFabManager.Instance.sv.totalSpeedLevel = DataHandler.instance.speedUp;
                PlayFabManager.Instance.sv.spawnPos = UIManager.Instance.spawnpos;
                PlayFabManager.Instance.sv.numberofTanks = UIManager.Instance.numberofTank;
           //*     GF_SaveLoad.SaveProgress();
            }
            DataHandler.instance.tankLevelCheck();
        }

        if (!levelComplete)
        {
            transform.LookAt(waypoints[wayPointIndex].position);
        }
    }

    public void Shoot()
    {
        if ((Time.fixedTime - lastTimeShoot) > this.timebetweenShoots)
        {
            lastTimeShoot = Time.fixedTime;
            GameObject newGameObject = Instantiate(rocket, rocketInstantiatePoint.transform.position, rocketInstantiatePoint.transform.rotation);
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

        if(tankRotateLimit > 3)
        {
            bodyRotationSpeed = -bodyRotationSpeed;
            tankRotateLimit = 0;
        }

        if (time > 0.2f)
        {
            speed = normalSpeed;
            timebetweenShoots = 1.5f;
            particle1.SetActive(false);
            particle2.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            time = 0;
            Time.timeScale = 1f;
            timebetweenShoots = .3f;
            tapping = true;
        }

        if (tapping)
        {
            speed += 15;
            particle1.SetActive(true);
            particle2.SetActive(true);
            tapping = false;
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
}
