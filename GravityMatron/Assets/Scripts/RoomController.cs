using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public static RoomController Instance;
    public int roomNum;
    public List<GameObject> rooms;

    public Room currentRoom;

    void Awake()
    {
        Instance = this;
        currentRoom = rooms[0].GetComponent<Room>();
    }

    public void Teleport()
    {
        ExitPrevious();
        currentRoom = rooms[roomNum].GetComponent<Room>();
        currentRoom.EnterRoom();
    }

    public void ExitPrevious()
    {
        Debug.Log(currentRoom);
        currentRoom.ExitRoom();
    }

}
