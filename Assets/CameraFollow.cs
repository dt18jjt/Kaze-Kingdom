using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public float RotationSpeed = 1;
    //public Transform Target, Player;
    //float mouseX, mouseY;
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
        Vector3 newPos = playerTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
    }
    private void LateUpdate()
    {
        //CamControl();
    }
    void CamControl()
    {
        //mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        //mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        //mouseY = Mathf.Clamp(mouseY, -35, 60);

        //transform.LookAt(Target);

        //Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        //Player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}
