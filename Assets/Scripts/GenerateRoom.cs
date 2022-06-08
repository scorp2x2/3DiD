using Assets.Scripts;
using GenerateLvl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GenerateRoom : MonoBehaviour
{
    public Transform roomElements;
    public RoomSave room;
    public Image back;

    public Tilemap wall;
    public Tilemap floor;
    public Tilemap door;

    public Tilemap hole;
    public Tilemap obstacle;
    public Tilemap trap;
    public Tilemap chest;

    public NavMeshSurface2d navMeshSurface2D;
    // Start is called before the first frame update

    public void GenerationEnemy(List<Personage> personages)
    {
        foreach (var item in personages)
        {
            PersonageSave personage = new PersonageSave(item);
            var pos = hole.layoutGrid.GetCellCenterWorld(room.GenerateEnemy());
            pos = new Vector3(pos.x / item.Prefab.transform.localScale.x, pos.y / item.Prefab.transform.localScale.y, pos.z);

            GameObject go = Instantiate(item.Prefab, pos, new Quaternion(), roomElements);
            go.GetComponent<PersonageController>().Personage = personage;
        }
    }

    public void Clear()
    {
        wall.ClearAllTiles();
        floor.ClearAllTiles();
        door.ClearAllTiles();
        obstacle.ClearAllTiles();
        hole.ClearAllTiles();
        trap.ClearAllTiles();
        chest.ClearAllTiles();

        for (int i = 0; i < roomElements.childCount; i++)
            Destroy(roomElements.GetChild(i).gameObject);
    }

    public void DrawTile(RoomSave roomSave)
    {
        this.room = roomSave;

        for (int i = 0; i < room.height; i++)
        {
            for (int j = 0; j < room.width; j++)
            {
                var elem = room.roomElements[i, j];

                if (elem is Floor)
                {
                    floor.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                }
                else if (elem is Wall)
                    wall.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                else if (elem is Door)
                {
                    door.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                    //room.doorControllers.Add
                    var go = elem.GetSprite(gameObject, new Vector2(i, j), room.width, room.height);
                    go.GetComponent<DoorController>().Vector2Int = new Vector2Int(j - 9, -i + 4);
                    var pos = door.layoutGrid.GetCellCenterWorld(new Vector3Int(j - 9, -i + 4, 0));


                    Instantiate(go, pos, new Quaternion(), roomElements);
                }
                else if (elem is Hole)
                    hole.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                else if (elem is Obstacle)
                    obstacle.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                else if (elem is Trap)
                {
                    //trap.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                    var pos = hole.layoutGrid.GetCellCenterWorld(new Vector3Int(j - 9, -i + 4, 0));
                    var go = elem.GetSprite(gameObject, new Vector2(i, j), room.width, room.height);
                    Instantiate(go, pos, new Quaternion(), roomElements);

                }
                else if (elem is Chest)
                {
                    chest.SetTile(new Vector3Int(j - 9, -i + 4, 0), elem.GetTile(new Vector2(i, j), room.width, room.height));
                    var pos = hole.layoutGrid.GetCellCenterWorld(new Vector3Int(j - 9, -i + 4, 0));
                    var go = elem.GetSprite(gameObject, new Vector2(i, j), room.width, room.height);
                    Instantiate(go, pos, new Quaternion(), roomElements);


                }
                // GameObject.Instantiate(room.roomElements[i, j].Get(prefabElement, new Vector2(i, j), width, height), transform);
            }
        }
        BuildMesh();
    }

    [ContextMenu("Buildmesh")]
    public void BuildMesh()
    {
        navMeshSurface2D.BuildNavMesh();

    }


    // Update is called once per frame
    void Update()
    {
        // layout.cellSize = new Vector2(layout.cellSize.x, (roomElements.GetComponent<RectTransform>()).rect.height / 9);
    }
}
