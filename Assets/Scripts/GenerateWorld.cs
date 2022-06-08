using Assets.Scripts;
using GenerateLvl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateWorld
{
    public static Room roomMain;

    public static RoomSave currentRoom;
    public static PersonageSave PersonageSave;
    static void Generate()
    {

        var roomStart = new RoomSave(roomMain);
        roomStart.Generate(18, 10);
    }

    public static void NextRoom(Vector2Int vector2Int)
    {

        currentRoom = new RoomSave(roomMain);
        currentRoom.Generate(18, 10, vector2Int);
    }

}
