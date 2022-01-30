using System;
using UnityEngine;

public class CameraLookZone : MonoBehaviour
{
    private CameraInterpolate _cam;

    private void Start()
    {
        if (Camera.main) _cam = Camera.main.GetComponent<CameraInterpolate>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var pos = transform.position;
            _cam.targetPosition = new Vector3(pos.x, pos.y, _cam.targetPosition.z);
        }
    }
}