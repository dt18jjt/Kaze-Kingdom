using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public bool taken;
    public float forceCap, forceAdd, sizeAdd, exitCooldown = 0f, mass;
    Transform center;
    // Start is called before the first frame update
    void Start()
    {
        center = GameObject.Find("Center").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //decrease exit cooldown
        if (exitCooldown > 0)
            exitCooldown -= Time.deltaTime;
        // setting objects layer 
        gameObject.layer = (taken) ? 8 : 9;
        if (!taken )
            GetComponent<Rigidbody>().AddForce(0, -10, 0);
        if (taken)
        {
            // when taken object is too far from player
            Vector3 randPos = new Vector3(Random.Range(center.position.x - 10, center.position.x + 10), Random.Range(center.position.y, center.position.y + 5),
                Random.Range(center.position.z - 10, center.position.z + 10));
            if(Vector3.Distance(gameObject.transform.position, center.position) > 15)
            {
                gameObject.transform.position = randPos;
            }
        }


    }
}
