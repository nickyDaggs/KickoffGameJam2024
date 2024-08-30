using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowManager : MonoBehaviour
{
    [System.Serializable]
    public class Room
    {
        public Sprite roomPic;
        public List<GameObject> objects;
        public List<bool> arrowsOn;
        public List<int> roomsOn;
    }

    int currentRoom = 0;

    public List<Room> allRooms = new List<Room>();

    public SpriteRenderer mainFloor;

    public List<Button> arrowList;

    public void MoveRoom(int room)
    {
        mainFloor.sprite = allRooms[room].roomPic;
        foreach(GameObject obj in allRooms[room].objects)
        {
            obj.SetActive(true);
        }
        currentRoom = room;
    }
}
