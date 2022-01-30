using System;
using UnityEngine;

public class CameraInterpolate : MonoBehaviour
{
    public float lerpRate = 3.0f;
    public Vector3 targetPosition;
    public bool follow;
    private GameObject _player;

    private void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
        
        targetPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            targetPosition = _player.transform.position;
        }
        var pos = Vector3.Lerp(transform.position, targetPosition, lerpRate*Time.fixedDeltaTime);
        pos.z = -10.0f; // don't slam the camera into the scene!
        transform.position = pos;
    }
}