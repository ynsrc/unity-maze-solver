using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public HingeJoint[] rearWheelJoints = new HingeJoint[2];
    public HingeJoint servoJoint;
    public SensorController sensorLeftController, sensorFrontController, sensorRightController;

    public float motorMoveForce = 500f;
    
    void SolveMaze()
    {
        float leftDistance = sensorLeftController.distance;
        float rightDistance = sensorRightController.distance;
        float frontDistance = sensorFrontController.distance;

        if (frontDistance < 15f)
        {

        }
    }

    void Update()
    {


        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

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
