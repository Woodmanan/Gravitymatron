using System;
using UnityEngine;

public class CameraLookZone : MonoBehaviour
{
    public bool setFollow;
    
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
            _cam.follow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && setFollow)
        {
            _cam.follow = true;
        }
    }
}