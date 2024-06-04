using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorDistanceDisplay : MonoBehaviour
{
    public SensorController sensorController;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        if (float.IsFinite(sensorController.distance))
        {
            text.text = sensorController.distance.ToString("0.00") + " cm";
        }
        else
        {
            text.text = "inf";
        }
    }
}
