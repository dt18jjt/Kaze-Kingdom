using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform tornadoCenter;
    public float pullForce, refreshRate;
    [SerializeField]
    private Rigidbody playerBody;
    private Vector3 inputVector;
    public Camera cam;
    public Transform camPos;
    public List<GameObject> objects = new List<GameObject>();
    Vector2 inputs;
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();

        
    }
    private void Update()
    {
        //setting input values
        inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputs = Vector2.ClampMagnitude(inputs, 1);
        //setting camera postion values
        Vector3 camF = camPos.forward;
        Vector3 camR = camPos.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
        transform.position += (camF * inputs.y + camR * inputs.x) * Time.deltaTime * 5;
        
    }
    private void FixedUpdate()
    {
        playerBody.velocity = inputVector;
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
                global.comboOn = true;
                global.comboNum += 1;
                global.comboTime = global.comboTimeAmt;
                ob.taken = true;
                //ob.GetComponent<BoxCollider>().isTrigger = true;
                pullForce += 10;
                //cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y+1, cam.transform.position.z + 1);
                cam.fieldOfView += 0.1f;
                transform.localScale = new Vector3(transform.localScale.x + ob.sizeAdd, transform.localScale.y + ob.sizeAdd, transform.localScale.z + ob.sizeAdd);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
                
            }
                
        }
        if (other.CompareTag("Shock"))
        {
            int rand = Random.Range(0, objects.Count);
            ObjectScript exitOb = objects[rand].gameObject.GetComponent<ObjectScript>();
            //GameObject exitOb = objects[rand].gameObject;
            exitOb.taken = false;
            exitOb.exitCooldown = 3f;
            objects.RemoveAt(rand);
            //Destroy(exitOb);
            Debug.Log("T");
            Destroy(other.gameObject);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "OBJ")
        {
            //ObjectScript ob = other.gameObject.GetComponent<ObjectScript>();
            StartCoroutine(pullObject(other, false));            
            //if (ob.forceCap <= pullForce && !ob.taken)
               
        }
        
    }
    IEnumerator pullObject(Collider x, bool shouldPull)
    {
        if (shouldPull)
        {
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
