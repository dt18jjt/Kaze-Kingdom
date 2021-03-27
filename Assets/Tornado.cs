﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public Transform tornadoCenter;
    public float pullForce, refreshRate;
    [SerializeField]
    public Camera cam;
    public Transform camPos;
    public List<GameObject> objects = new List<GameObject>();
    Vector2 inputs;
    private void Start()
    {
        
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
            ObjectScript exitOb = objects[rand].gameObject.GetComponent<ObjectScript>(); // get random object from list
            List<int> randFlyList = new List<int> { -700, -500, 500, 700 };
            int randX = randFlyList[Random.Range(0, randFlyList.Count)];
            int randZ = randFlyList[Random.Range(0, randFlyList.Count)];
            exitOb.taken = false; // object is no longer taken
            exitOb.exitCooldown = 3f; // cooldown before being picked up again
            exitOb.gameObject.GetComponent<Rigidbody>().AddForce(randX, 500, randZ);
            objects.RemoveAt(rand); // remove object from list
            Destroy(other.gameObject);
        }
        
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
