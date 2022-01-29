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
    }

    public void Teleport()
    {
        ExitPrevious();
        currentRoom = rooms[roomNum].GetComponent<Room>();
        currentRoom.EnterRoom();
    }

    public void ExitPrevious()
    {
        currentRoom?.ExitRoom();
        currentRoom = null;
    }

}
