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

    private LineRenderer frontLineRenderer, leftLineRenderer, rightLineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        frontLineRenderer = sensorFrontTransform.GetComponent<LineRenderer>();
        leftLineRenderer = sensorLeftTransform.GetComponent<LineRenderer>();
        rightLineRenderer = sensorRightTransform.GetComponent <LineRenderer>();

        frontLineRenderer.startWidth = frontLineRenderer.endWidth = 0.2f;
        leftLineRenderer.startWidth = leftLineRenderer.endWidth = 0.2f;
        rightLineRenderer.startWidth = rightLineRenderer.endWidth = 0.2f;

        frontLineRenderer.startColor = frontLineRenderer.endColor = Color.green;
        leftLineRenderer.startColor = leftLineRenderer.endColor = Color.green;
        rightLineRenderer.startColor = rightLineRenderer.endColor = Color.green;

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

        var frontRay = new Ray(sensorFrontTransform.position, sensorFrontTransform.forward);
        var leftRay = new Ray(sensorLeftTransform.position, sensorLeftTransform.forward);
        var rightRay = new Ray(sensorRightTransform.position, sensorRightTransform.forward);

        frontLineRenderer.SetPosition(0, frontRay.origin);
        frontLineRenderer.SetPosition(1, frontRay.origin + frontRay.direction * rayLength);
        frontLineRenderer.startColor = frontLineRenderer.endColor = Physics.Raycast(frontRay, rayLength) ? Color.red : Color.green;

        leftLineRenderer.SetPosition(0, leftRay.origin);
        leftLineRenderer.SetPosition(1, leftRay.origin + leftRay.direction * rayLength);
        leftLineRenderer.startColor = leftLineRenderer.endColor = Physics.Raycast(leftRay, rayLength) ? Color.red : Color.green;

        rightLineRenderer.SetPosition(0, rightRay.origin);
        rightLineRenderer.SetPosition(1, rightRay.origin + rightRay.direction * rayLength);
        rightLineRenderer.startColor = rightLineRenderer.endColor = Physics.Raycast(rightRay, rayLength) ? Color.red : Color.green;
    }
}
