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
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();

        
    }
    private void Update()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal")*5f, -playerBody.velocity.y, Input.GetAxis("Vertical")*5f);
        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        
    }
    private void FixedUpdate()
    {
        playerBody.velocity = inputVector;
    }
    private void OnTriggerEnter(Collider other)
    {
        Global global = GameObject.Find("G").GetComponent<Global>();
        ObjectScript ob = other.gameObject.GetComponent<ObjectScript>();
        if (other.gameObject.tag == "OBJ")
        {
            if (ob.forceCap <= pullForce && !ob.taken)
            {
                StartCoroutine(pullObject(other, true));
                global.comboOn = true;
                global.comboNum += 1;
                global.comboTime = global.comboTimeAmt;
                ob.taken = true;
                //ob.GetComponent<BoxCollider>().isTrigger = true;
                pullForce += 10;
                //cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y+1, cam.transform.position.z + 1);
                cam.fieldOfView += 1;
                transform.localScale = new Vector3(transform.localScale.x + ob.sizeAdd, transform.localScale.y + ob.sizeAdd, transform.localScale.z + ob.sizeAdd);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            }
                
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
            Vector3 forceDir = tornadoCenter.position - x.transform.position;
            x.GetComponent<Rigidbody>().AddForce(forceDir.normalized * pullForce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(x, shouldPull));
        }
    }
}
