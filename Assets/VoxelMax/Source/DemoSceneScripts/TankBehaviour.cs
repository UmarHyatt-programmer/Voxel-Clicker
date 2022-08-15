using UnityEngine;
namespace VoxelMax
{
    public class TankBehaviour : MonoBehaviour
    {
        public Transform[] waypoints;
        public int waypointIndex;
        float distance;

        public float moveSpeed = 5f;
        public float bodyRotationSpeed = 30f;
        public float turretRotationSpeed = 30f;
        public float timebetweenShoots = 2f;
        public GameObject turret = null;
        public GameObject rocket = null;
        public GameObject rocketInstantiatePoint = null;
        private float lastTimeShoot = 0f;
        // Use this for initialization
        float x;
        float time = 0;
        float v;
        public GameObject movePoint;
        public GameObject building;
        public Transform rotPoint;

        bool isdetecting;
        public GameObject raycastPoint;
        void Start()
        {
            waypointIndex = 0;


            x = moveSpeed;
            v = moveSpeed + 10f;
            a = true;
            b = true;
        }

        bool taping;
        bool a, b;
        bool stopRotating;
        public int rotate = 20;
        float yRotation;
        RaycastHit hit;

        // Update is called once per frame
        async void Update()
        {
            if (Physics.Raycast(raycastPoint.transform.position, raycastPoint.transform.forward, out hit))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.gameObject.name == "hiii")
                {
                    turret.transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
                }
            }

            yRotation = this.transform.eulerAngles.y;
            if (!isdetecting)
            {
                this.gameObject.transform.position += this.gameObject.transform.TransformVector(Vector3.right) * moveSpeed * Time.deltaTime;

            }
            else
            {
                if (!stopRotating)
                {
                    transform.rotation = rotPoint.rotation;
                    stopRotating = true;

                }
                //turret.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Lerp(5, 55, Mathf.PingPong(Time.time * rotate, 1)));
                transform.localEulerAngles = new Vector3(0, Mathf.Lerp(35, 65, Mathf.PingPong(Time.time * bodyRotationSpeed, 1)), 0);
                Shoot();

                if (yRotation <= 25 || yRotation >= 155)
                {
                    yRotation += -bodyRotationSpeed;

                }

                //rotatingTurretandTank();
            }

            time += Time.deltaTime;

            if (time > .2f)
            {
                moveSpeed = x;
                timebetweenShoots = 2f;
            }

            if (Input.GetMouseButtonDown(0))
            {
                time = 0;
                timebetweenShoots = .3f;
                taping = true;
            }

            if (taping)
            {
                moveSpeed += 10f;
                taping = false;
            }


            if (moveSpeed > v)
            {
                moveSpeed = v;
            }


            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.position += this.gameObject.transform.TransformVector(Vector3.right) * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.position -= this.gameObject.transform.TransformVector(Vector3.right) * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.Rotate(Vector3.up, -this.bodyRotationSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.Rotate(Vector3.up, this.bodyRotationSpeed * Time.deltaTime);
            }
            if (turret != null)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    this.turret.transform.Rotate(Vector3.up, -this.turretRotationSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.E))
                {
                    this.turret.transform.Rotate(Vector3.up, this.turretRotationSpeed * Time.deltaTime);
                }
            }
            if ((rocket != null) && (rocketInstantiatePoint != null))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    this.Shoot();
                }
            }
        }

        public void Shoot()
        {
            if ((Time.fixedTime - lastTimeShoot) > this.timebetweenShoots)
            {
                lastTimeShoot = Time.fixedTime;
                GameObject newGameObject = Instantiate(rocket);
                newGameObject.transform.position = rocketInstantiatePoint.transform.position;
                newGameObject.transform.Rotate(Vector3.left, turret.transform.rotation.eulerAngles.y);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            isdetecting = true;
        }

        void rotatingTurretandTank()
        {

            if (turret.transform.GetChild(0).transform.position.y > -15f)
            {
                if (a)
                {
                    rotate = -20;
                    a = false;
                    b = true;
                }
            }
            else if (turret.transform.GetChild(0).transform.position.y < -45f)
            {
                if (b)
                {
                    rotate = 20;

                    b = false;
                    a = true;
                }

            }

            turret.transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
        }

        void wayPointMovement()
        {

        }


    }
}