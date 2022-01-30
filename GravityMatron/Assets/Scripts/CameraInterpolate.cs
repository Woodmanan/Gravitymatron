using System;
using UnityEngine;

public class CameraInterpolate : MonoBehaviour
{
    public float slerpRate = 1.0f;
    public Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, targetPosition, slerpRate*Time.deltaTime);
    }
}