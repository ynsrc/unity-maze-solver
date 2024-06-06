using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassNeedleController : MonoBehaviour
{
    public Transform robotTransform;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var eulerAngles = rectTransform.localEulerAngles;
        eulerAngles.z = robotTransform.eulerAngles.y;
        rectTransform.localEulerAngles = eulerAngles;
    }
}
