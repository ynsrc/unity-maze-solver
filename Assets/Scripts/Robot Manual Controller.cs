using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManualController : MonoBehaviour
{
    public WheelCollider leftWheelCollider;
    public WheelCollider rightWheelCollider;

    // Start is called before the first frame update
    void Start()
    {
        leftWheelCollider.brakeTorque = 0f;
        rightWheelCollider.brakeTorque = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            leftWheelCollider.motorTorque = 200f;
            rightWheelCollider.motorTorque = 200f;   
        } else if (Input.GetKey(KeyCode.A))
        {
            leftWheelCollider.motorTorque = -200f;
            rightWheelCollider.motorTorque = 200f;
        } else if (Input.GetKey(KeyCode.D))
        {
            leftWheelCollider.motorTorque = 200f;
            rightWheelCollider.motorTorque = -200f;
        } else if (Input.GetKey(KeyCode.Q))
        {
            leftWheelCollider.motorTorque = 150f;
            rightWheelCollider.motorTorque = 200f;
        } else if (Input.GetKey(KeyCode.E))
        {
            leftWheelCollider.motorTorque = 200f;
            rightWheelCollider.motorTorque = 150f;
        } else
        {
            leftWheelCollider.motorTorque = 0f;
            rightWheelCollider.motorTorque = 0f;
        }
    }
}
