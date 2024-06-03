using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManualController : MonoBehaviour
{
    public WheelCollider rearLeftWheelCollider, rearRightWheelCollider;
    public WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    public Transform frontPartTransform;
    public Transform sensorFrontTransform, sensorLeftTransform, sensorRightTransform;

    public float motorMoveForce = 500f;
    public float steeringAngle = 30f;
    public float rayLength = 15f;

    private Camera mainCamera;
    private Vector3 mainCameraDistances;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCameraDistances = transform.position - mainCamera.transform.position;

        rearLeftWheelCollider.brakeTorque = 0f;
        rearRightWheelCollider.brakeTorque = 0f;
        frontLeftWheelCollider.brakeTorque = 0f;
        frontRightWheelCollider.brakeTorque = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        if (vAxis != 0f)
        {
            rearLeftWheelCollider.motorTorque = motorMoveForce * vAxis;
            rearRightWheelCollider.motorTorque = motorMoveForce * vAxis;
        }

        Vector3 frontPartRotation = frontPartTransform.localEulerAngles;

        if (hAxis != 0f)
        {
            frontPartRotation.y = steeringAngle * hAxis;
        }
        else if (frontPartRotation.y != 0f)
        {
            frontPartRotation.y = 0f;
        }
        
        frontPartTransform.localEulerAngles = frontPartRotation;

        mainCamera.transform.position = transform.position - mainCameraDistances;

        var frontRay = new Ray(sensorFrontTransform.position, sensorFrontTransform.forward);
        var leftRay = new Ray(sensorLeftTransform.position, sensorLeftTransform.forward);
        var rightRay = new Ray(sensorRightTransform.position, sensorRightTransform.forward);

        Debug.DrawRay(frontRay.origin, frontRay.direction * rayLength, Physics.Raycast(frontRay, rayLength) ? Color.red : Color.green);
        Debug.DrawRay(leftRay.origin, leftRay.direction * rayLength, Physics.Raycast(leftRay, rayLength) ? Color.red : Color.green);
        Debug.DrawRay(rightRay.origin, rightRay.direction * rayLength, Physics.Raycast(rightRay, rayLength) ? Color.red : Color.green);
    }
}
