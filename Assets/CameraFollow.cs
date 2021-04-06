using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player, Rotator;
    public Transform playerTransform;
    private Vector3 cameraOffset;
    public Transform camTransform;
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;
    // How long the object should shake for.
    public float shakeDuration = 0f;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - playerTransform.position;
        camTransform = GetComponent<Transform>();
        originalPos = camTransform.localPosition;
    }
    private void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Rotator.position = Player.transform.position;
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
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
