using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diasterScript : MonoBehaviour
{
    Tornado player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Tornado>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shock"))
        {
            player.loseObject();
            Destroy(other.gameObject);
            StartCoroutine(player.Flash());
        }
        if (other.CompareTag("Reverse"))
        {
            player.loseObject();
            Destroy(other.gameObject);
            player.tremor();
        }
        if (other.CompareTag("Slow"))
        {
            player.loseObject();
            Destroy(other.gameObject);
            player.avalancheCooldown = 10f; // slower movement
        }
    }
}
