
using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GenerateLvl
{
    [CreateAssetMenu]
    public class Room : ScriptableObject
    {
        public Wall Wall;
        public Floor Floor;
        public Hole Hole;
        public Door Door;
        public Chest Chest;
        public Obstacle Obstacle;
        public GameObject prefabElement;

        public Sprite Background;

        public Trap trap;

        public List<Personage> Personages;
    }

    public class RoomSave
    {
        public RoomElement[,] roomElements;
        public Room room;

        public int height;
        public int width;



        public RoomSave(Room room)
        {
            this.room = room;
        }

        public void Generate(int width, int height)
        {
            this.width = width;
            this.height = height;
            roomElements = new RoomElement[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                        roomElements[i, j] = room.Wall;
                    else
                        roomElements[i, j] = room.Floor;
                }

            }

            GenerateDoor();
            GenerateHole(Random.Range(2, 5));
            GenerateObstacle(Random.Range(2, 5));
            GenerateChest(Random.Range(0, 3));

            GenerationTrap(Random.Range(0, 3));

            GenerateEnemy();
        }

        public enum CloseDoor
        {
            up, down, left, right
        }
        public CloseDoor closeDoor;
        public void Generate(int width, int height, Vector2 Vector2Door)
        {
            if (Vector2Door.y == 0)
                closeDoor = CloseDoor.down;
            else if (Vector2Door.y == -5)
                closeDoor = CloseDoor.up;
            else if (Vector2Door.x == 0)
                closeDoor = CloseDoor.right;
            else
                closeDoor = CloseDoor.left;

            Generate(width, height);

        }

        public Vector3Int GenerateEnemy()
        {
            while (true)
            {
                int x = Random.Range(1, width - 1);
                int y = Random.Range(1, height - 1);
                if (!CheckVector2(new Vector2(y, x)))
                    return new Vector3Int(x, y, 0);
            }
        }

        public void GenerationTrap(int count)
        {
            for (int i = 0; i < count;)
            {
                var x = Random.Range(1, height - 1);
                var y = Random.Range(1, width - 1);

                var Vector2 = new Vector2(x, y);

                if (!CheckVector2(Vector2))
                {
                    roomElements[x, y] = room.trap;
                    i++;
                }
            }
        }

        public void GenerateDoor()
        {

            if (closeDoor != CloseDoor.left)
                AddDoor(new Vector2(Random.Range(2, height - 2), 0));
            if (closeDoor != CloseDoor.down)
                AddDoor(new Vector2(Random.Range(2, height - 2), width - 1));

            if (closeDoor != CloseDoor.right)
                AddDoor(new Vector2(height - 1, Random.Range(1, height - 1)));
            if (closeDoor != CloseDoor.up)
                AddDoor(new Vector2(0, Random.Range(1, width - 1)));

        }

        public void AddDoor(Vector2 Vector2)
        {
            roomElements[(int)Vector2.x, (int)Vector2.y] = room.Door;
        }

        public void GenerateHole(int count)
        {
            for (int i = 0; i < count;)
            {
                var x = Random.Range(1, height - 1);
                var y = Random.Range(1, width - 1);

                var Vector2 = new Vector2(x, y);

                if (!CheckVector2(Vector2))
                {
                    roomElements[x, y] = room.Hole;
                    i++;
                }
            }
        }

        public void GenerateObstacle(int count)
        {

            for (int i = 0; i < count;)
            {
                var x = (Random.Range(1, height - 1));
                var y = (Random.Range(1, width - 1));

                var Vector2 = new Vector2(x, y);

                if (!CheckVector2(Vector2))
                {
                    roomElements[x, y] = room.Obstacle;
                    i++;
                }
            }
        }

        public void GenerateChest(int count)
        {
            for (int i = 0; i < count;)
            {
                var x = (Random.Range(1, height - 1));
                var y = (Random.Range(1, width - 1));

                var Vector2 = new Vector2(x, y);

                if (!CheckVector2(Vector2))
                {
                    roomElements[x, y] = room.Chest;
                    i++;
                }
            }
        }

        bool CheckVector2(Vector2 Vector2)
        {
            if (roomElements[0, (int)Vector2.y] is Door)
                return true;

            if (roomElements[height - 1, (int)Vector2.y] is Door)
                return true;

            if (roomElements[(int)Vector2.x, 0] is Door)
                return true;

            if (roomElements[(int)Vector2.x, width - 1] is Door)
                return true;

            if ((roomElements[(int)Vector2.x, (int)Vector2.y] is Floor))
                return false;

            return true;
        }
    }
}
