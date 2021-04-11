using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform tornadoCenter;
    public float pullForce, refreshRate, score, shockCooldown = 0f, tremorCooldown = 0f, avalancheCooldown = 0f, reverseNum, speed, normalSpeed;
    public bool camScale, Scale;
    [SerializeField]
    public GameObject cam;
    public Transform camPos;
    public List<GameObject> objects = new List<GameObject>();
    Vector2 inputs;
    Vector3 newCamScale, newScale;
    public GameObject flashEffect, slowEffect;
    CameraFollow cameraFollow;
    private void Start()
    {
        reverseNum = 1;
        cameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
    }
    private void Update()
    {
        //setting input values
        inputs = new Vector2(Input.GetAxis("Horizontal") * reverseNum, Input.GetAxis("Vertical") * reverseNum);
        inputs = Vector2.ClampMagnitude(inputs, 1);
        //setting camera postion values
        Vector3 camF = camPos.forward;
        Vector3 camR = camPos.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
        if (shockCooldown <= 0)
            transform.position += (camF * inputs.y + camR * inputs.x) * Time.deltaTime * speed;
        if (shockCooldown > 0)
            shockCooldown -= Time.deltaTime;
        if (tremorCooldown > 0)
            tremorCooldown -= Time.deltaTime;
        if (avalancheCooldown > 0)
            avalancheCooldown -= Time.deltaTime;
        reverseNum = (tremorCooldown > 0) ? -1 : 1;
        speed = (avalancheCooldown > 0) ? normalSpeed / 2 : normalSpeed;
        slowEffect.SetActive((avalancheCooldown > 0) ? true : false);
        //Camera scale up
        if (camScale)
        {
            cam.transform.localScale = Vector3.Lerp(cam.transform.localScale, newCamScale, Time.deltaTime);
            if (Vector3.Distance(cam.transform.localScale, newCamScale) < 0.001f)
            {
                cam.transform.localScale = newCamScale;
                //hero = null;
                camScale = false;

            }

        }
        //Tornado Scale up
        if (Scale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime);
            if (Vector3.Distance(transform.localScale, newScale) < 0.001f)
            {
                transform.localScale = newScale;
                //hero = null;
                Scale = false;

            }

        }
    }
    void loseObject()
    {
        int rand = Random.Range(0, objects.Count);
        ObjectScript exitOb = objects[rand].gameObject.GetComponent<ObjectScript>(); // get random object from list
        List<int> randFlyList = new List<int> { -700, -500, 500, 700 };
        int randX = randFlyList[Random.Range(0, randFlyList.Count)];
        int randZ = randFlyList[Random.Range(0, randFlyList.Count)];
        exitOb.taken = false; // object is no longer taken
        exitOb.exitCooldown = 3f; // cooldown before being picked up again
        exitOb.gameObject.GetComponent<Rigidbody>().AddForce(randX, 500, randZ);
        objects.RemoveAt(rand); // remove object from list
        //GameObject h = Instantiate(hitEffect, tornadoCenter.position, Quaternion.identity);
        //Destroy(h, 0.6f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Global global = GameObject.Find("G").GetComponent<Global>();
        ObjectScript ob = other.gameObject.GetComponent<ObjectScript>();
        if (other.CompareTag("OBJ"))
        {
            if (ob.forceCap <= pullForce && !ob.taken && ob.exitCooldown <= 0)
            {
                objects.Add(other.gameObject);
                StartCoroutine(pullObject(other, true));
                //add to combo
                global.comboOn = true;
                global.comboNum += 1;
                global.comboTime = global.comboTimeAmt;
                ob.taken = true;
                //add to score
                score += ob.scoreAdd;
                //increase pulling force
                pullForce += ob.forceAdd;
                //new camera scale
                camScale = true;
                newCamScale = new Vector3(cam.transform.localScale.x + (ob.sizeAdd/10), cam.transform.localScale.y + (ob.sizeAdd/10), 
                    cam.transform.localScale.z + (ob.sizeAdd/10));
                //cam.transform.localScale = Vector3.Lerp(cam.transform.localScale, newScale, 2*Time.deltaTime);
                //new tornado scale
                Scale = true;
                newScale = new Vector3(transform.localScale.x + ob.sizeAdd, transform.localScale.y + ob.sizeAdd, transform.localScale.z + ob.sizeAdd);
                //increase speed
                normalSpeed += ob.sizeAdd;
                //object is no long stationary
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                
            }
                
        }
        if (other.CompareTag("Shock"))
        {
            loseObject();
            Destroy(other.gameObject);
            StartCoroutine(Flash());
            shockCooldown = 3f; // player is stunned
        }
        if (other.CompareTag("Reverse"))
        {
            loseObject();
            Destroy(other.gameObject);
            cameraFollow.shakeDuration = 1f;
            tremorCooldown = 5f; // movement is reversed
        }
        if (other.CompareTag("Slow"))
        {
            loseObject();
            Destroy(other.gameObject);
            avalancheCooldown = 10f; // slower movement
        }


    }
    IEnumerator Flash()
    {
        flashEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        flashEffect.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "OBJ")
        {
            StartCoroutine(pullObject(other, false));            
        }
        
    }
    IEnumerator pullObject(Collider x, bool shouldPull)
    {
        if (shouldPull)
        {
            //pull each object in object list
            //Vector3 forceDir = tornadoCenter.position - x.transform.position;
            foreach(GameObject i in objects)
            {
                Vector3 forceDir = tornadoCenter.position - i.transform.position;
                i.GetComponent<Rigidbody>().AddForce(forceDir.normalized * pullForce * Time.deltaTime);
            }
            //x.GetComponent<Rigidbody>().AddForce(forceDir.normalized * pullForce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(x, shouldPull));
        }
    }
}
