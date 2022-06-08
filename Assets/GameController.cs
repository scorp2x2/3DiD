using Assets.Scripts;
using GenerateLvl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PersonageSave Personage;
    public List<Personage> Enemy;

    public bool isDeath;

    public Personage PersonageTest;
    public Room RoomTest;
    private void Awake()
    {
        Personage = new PersonageSave(PersonageTest);
        GenerateWorld.currentRoom = new RoomSave(RoomTest);
        GenerateWorld.currentRoom.Generate(18, 10);

        var genRoom = FindObjectOfType<GenerateRoom>();
        genRoom.DrawTile(GenerateWorld.currentRoom);

        FindObjectOfType<GenerateRoom>().GenerationEnemy(new List<Personage>()
        {
            Enemy.GetRandom(),
            Enemy.GetRandom()
        });
    }

    public void NextRoom(Vector2Int vector2Int)
    {
        GenerateWorld.currentRoom = new RoomSave(RoomTest);
        GenerateWorld.currentRoom.Generate(18, 10, vector2Int);

        FindObjectOfType<GenerateRoom>().Clear();
        FindObjectOfType<GenerateRoom>().DrawTile(GenerateWorld.currentRoom);

        //var pos = FindObjectOfType<GenerateRoom>().hole.layoutGrid.GetCellCenterWorld(new Vector3Int(0, 0, 0));

        //FindObjectOfType<PlayerController>().Teleport(pos);

        FindObjectOfType<GenerateRoom>().GenerationEnemy(new List<Personage>()
        {
            Enemy.GetRandom(),
            Enemy.GetRandom()
        });

    }
}
