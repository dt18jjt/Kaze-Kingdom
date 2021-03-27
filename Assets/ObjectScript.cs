using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public bool taken;
    public float forceCap, forceAdd, sizeAdd, exitCooldown = 0f, mass;
    Rigidbody rigidbody;
    Transform center;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        center = GameObject.Find("Center").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (exitCooldown > 0)
        {
            exitCooldown -= Time.deltaTime;
            rigidbody.AddForce(0, -15, 0);
        }
        if (taken)
        {
            Vector3 randPos = new Vector3(Random.Range(center.position.x - 10, center.position.x + 10), Random.Range(center.position.y, center.position.y + 5),
                Random.Range(center.position.z - 10, center.position.z + 10));
            if(Vector3.Distance(gameObject.transform.position, center.position) > 10)
            {
                gameObject.transform.position = randPos;
            }
        }


    }
}
