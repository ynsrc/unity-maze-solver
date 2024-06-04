using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManualController : MonoBehaviour
{
    public WheelCollider[] rearWheels = new WheelCollider[2];
    public Transform frontPartTransform;
    public SensorController sensorLeft, sensorFront, sensorRight;

    public float motorMoveForce = 500f;
    public float steeringAngle = 30f;

    void Start()
    {
        foreach (WheelCollider wheelCollider in GetComponentsInChildren<WheelCollider>())
        {
            wheelCollider.brakeTorque = 0f;
        }
    }
    
    void Update()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        if (vAxis != 0f)
        {
            foreach (WheelCollider wheelCollider in rearWheels)
            {
                wheelCollider.motorTorque = motorMoveForce * vAxis;
            }
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

        foreach (WheelCollider wheelCollider in GetComponentsInChildren<WheelCollider>())
        {
            var tireTransform = wheelCollider.transform.GetChild(0).transform;
            var rotation = tireTransform.localEulerAngles;
            rotation.x += wheelCollider.rpm / 60 * 360 * Time.deltaTime;
            tireTransform.localEulerAngles = rotation;
        }
    }
}
