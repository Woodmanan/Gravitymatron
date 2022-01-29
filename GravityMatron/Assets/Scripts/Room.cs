using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Camera _camera;
    private Transform _respawnPosition;

    private void Start()
    {
        _camera = Camera.main;
        _respawnPosition = transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Snap camera to room
            var pos = transform.position;
            var cameraTransform = _camera.transform;
            cameraTransform.position = new Vector3(pos.x, pos.y, cameraTransform.position.z);
            
            // Set player respawn point
            other.GetComponent<PlayerController>().respawnPosition = _respawnPosition.position;
        }
    }
}