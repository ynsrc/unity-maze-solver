using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    public float rayLength = 15f;
    public float lineStartWidth = 0.2f;
    public float lineEndWidth = 0.2f;

    [HideInInspector]
    public float distance = float.PositiveInfinity;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineStartWidth;
        lineRenderer.endWidth = lineEndWidth;
        lineRenderer.startColor = lineRenderer.endColor = Color.green;
    }

    void Update()
    {
        var ray = new Ray(transform.position, transform.forward);
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.origin + ray.direction * rayLength);
        lineRenderer.startColor = lineRenderer.endColor = Physics.Raycast(ray, out RaycastHit hitInfo, rayLength) ? Color.red : Color.green;
        distance = hitInfo.collider != null ? hitInfo.distance : float.PositiveInfinity;
    }
}
