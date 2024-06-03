using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform robotTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(robotTransform.position.x, transform.position.y, robotTransform.position.z);
    }
}
