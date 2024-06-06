using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public HingeJoint[] rearWheelJoints = new HingeJoint[2];
    public HingeJoint servoJoint;
    public SensorController sensorLeftController, sensorFrontController, sensorRightController;

    public float motorMoveForce = 500f;

    private float vAxis, hAxis = 0.0f;
    
    void SolveMaze()
    {
        float leftDistance = sensorLeftController.distance;
        float rightDistance = sensorRightController.distance;
        float frontDistance = sensorFrontController.distance;

        if (frontDistance < 15f)
        {
            if (rightDistance < 15f)
            {
                vAxis = 1f;
                hAxis = -1f;
            }
            else
            {
                if (leftDistance > 15f)
                {
                    vAxis = -1f;
                    hAxis = 1f;
                }
                else
                {
                    vAxis = 1f;
                    hAxis = 1f;
                }
            }
        }
        else
        {
            if (rightDistance > 10f)
            {
                vAxis = 1f;
                hAxis = 1f;
            }
            else
            {
                if (rightDistance < 6f)
                {
                    vAxis = 1f;
                    hAxis = -0.5f;
                }
                else if (leftDistance < 6f)
                {
                    vAxis = 1f;
                    hAxis = 0.5f;
                }
                else
                {
                    vAxis = 1f;
                    hAxis = 0f;
                }
            }
        }
    }

    void Update()
    {
        SolveMaze();

        float vAxisInput = Input.GetAxis("Vertical");
        float hAxisInput = Input.GetAxis("Horizontal");

        if (vAxisInput != 0f) vAxis = vAxisInput;
        if (hAxisInput != 0f) hAxis = hAxisInput;

        if (vAxis != 0f)
        {
            foreach (HingeJoint hingeJoint in rearWheelJoints)
            {
                var jointMotor = hingeJoint.motor;
                jointMotor.force = motorMoveForce;
                jointMotor.targetVelocity = vAxis * motorMoveForce;
                hingeJoint.motor = jointMotor;
            }
        }
        else
        {
            foreach (HingeJoint hingeJoint in rearWheelJoints)
            {
                var jointMotor = hingeJoint.motor;
                jointMotor.force = 0;
                jointMotor.targetVelocity = 0;
                hingeJoint.motor = jointMotor;
            }
        }

        if (hAxis != 0f)
        {
            var servoMotor = servoJoint.motor;
            servoMotor.force = motorMoveForce;
            servoMotor.targetVelocity = hAxis * motorMoveForce;
            servoJoint.motor = servoMotor;
            servoJoint.useMotor = true;
            servoJoint.useSpring = false;
        }
        else
        {
            servoJoint.useMotor = false;
            servoJoint.useSpring = true;
        }
    }
}
