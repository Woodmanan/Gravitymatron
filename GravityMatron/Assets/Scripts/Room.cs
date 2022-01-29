using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> traps;

    private Camera _camera;
    private Transform _respawnPosition;
    private PlayerController _player;

    private void Start()
    {
        _camera = Camera.main;
        _respawnPosition = transform.GetChild(0);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        // Deactivate all traps
        foreach (GameObject obj in traps)
        {
            obj.SetActive(false);
        }
    }

    public void EnterRoom()
    {
        // Exit previous room
        RoomController.Instance.ExitPrevious();
        RoomController.Instance.currentRoom = this;

        // Snap camera to room
        var pos = transform.position;
        var cameraTransform = _camera.transform;
        cameraTransform.position = new Vector3(pos.x, pos.y, cameraTransform.position.z);

        // Set player respawn point
        _player.respawnPosition = _respawnPosition.position;

        // Activate all traps
        foreach (GameObject obj in traps)
        {
            obj.SetActive(true);
        }
    }

    public void ExitRoom()
    {
        // Deactivate all traps
        foreach (GameObject obj in traps)
        {
            obj.SetActive(false);
        }
    }
}