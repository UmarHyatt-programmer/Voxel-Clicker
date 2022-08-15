using UnityEngine;
using System.Collections;

namespace VoxelMax
{
    public class Rocket : MonoBehaviour
    {
        public float speed = 20f;

        public GameObject collisionParticle;
        public GameObject collisionParticle2;

        int random;
        

        // Update is called once per frame
        void FixedUpdate()
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.up)  * speed);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("building"))
            {
                if(GameManager.instance.sfx)
                {
                    GameObject sound = new GameObject("Sound");
                    sound.transform.position = this.transform.position;

                    sound.AddComponent<AudioSource>().clip = gameObject.GetComponent<AudioSource>().clip;
                    sound.GetComponent<AudioSource>().volume = .1f;
                    sound.GetComponent<AudioSource>().Play();

                    Destroy(sound, 4);
                }
               
                // gameObject.GetComponent<AudioSource>().Play();
                VoxelBomb bomb = this.gameObject.GetComponent<VoxelBomb>();
                random = Random.Range(1, 10);


                if (random > 5)
                {
                    GameObject obj = Instantiate(collisionParticle, this.gameObject.transform.position, this.gameObject.transform.rotation);
                    obj.transform.parent = Camera.main.gameObject.transform;
                    Destroy(obj, 5);
                }
                else
                {
                    GameObject obj = Instantiate(collisionParticle2, this.gameObject.transform.position, this.gameObject.transform.rotation);
                    obj.transform.parent = Camera.main.transform;
                    Destroy(obj, 5);
                }


                if (bomb != null)
                {
                    bomb.triggered = true;
                }

                if (other.gameObject.GetComponent<BuildingHealth>().bulletAction == 0)
                {
                    other.gameObject.GetComponent<BuildingHealth>().bulletAction = 20;
                }
            }
        }
    }
}