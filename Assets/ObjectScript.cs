using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public bool taken;
    public float forceCap, forceAdd, sizeAdd, exitCooldown = 0f;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (exitCooldown > 0)
        {
            exitCooldown -= Time.deltaTime;
            //rigidbody.mass *= 100;
        }
            

    }
}
