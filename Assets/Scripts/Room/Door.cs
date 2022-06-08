using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GenerateLvl
{
    [CreateAssetMenu(menuName = "RoomElement/Door")]
    public class Door : RoomElement
    {
        public override string symbol { get { return "D "; } }

        public List<Tile> Tiles;
        public GameObject prefab;


        public override GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            return prefab;
        }

        public override Tile GetTile(Vector2 vector, int width, int height)
        {
            if (vector.x == 0)
                return Tiles[0];
            if (vector.x == height - 1)
                return Tiles[2];
            if (vector.y == 0 && vector.x != 0)
                return Tiles[3];
            if (vector.y == width - 1 && vector.x != height - 1)
                return Tiles[1];

            return null;
        }
    }
}
