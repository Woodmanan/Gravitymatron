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
        SwitchRoom(rooms[roomNum].GetComponent<Room>());
    }

    public void SwitchRoom(Room room)
    {
        if (currentRoom == room)
        {
            return;
        }

        currentRoom.ExitRoom();
        currentRoom = room;
        currentRoom.PrepRoom();

    }

}
