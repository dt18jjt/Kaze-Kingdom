using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player, Rotator;
    public Transform playerTransform;
    private Vector3 cameraOffset;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Rotator.position = Player.transform.position;
    }
    private void LateUpdate()
    {
        CamControl();
    }
    void CamControl()
    {
        //camera rotates left
        if(Input.GetAxis("Mouse X") < 0)
            Rotator.Rotate(0, -5, 0);
        //camera rotates right
        if (Input.GetAxis("Mouse X") > 0)
            Rotator.Rotate(0, 5, 0);
    }
}
