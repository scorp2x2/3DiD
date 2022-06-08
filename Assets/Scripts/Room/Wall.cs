using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GenerateLvl
{
    [CreateAssetMenu(menuName = "RoomElement/Wall")]
    public class Wall: RoomElement
    {
        public override string symbol { get { return "[]"; } }

        public List<Tile> Tiles;
        public List<Tile> TilesCorner;

        public Sprite Corner;
        public GameObject prefab;

        public override GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            if (vector.x == 0 && vector.y == 0)
                prefab.GetComponent<Image>().sprite = Corner;
            else if (vector.x == 0 && vector.y == width - 1)
            {
                prefab.GetComponent<Image>().sprite = Corner;
            }
            else if (vector.x == height - 1 && vector.y == 0)
                prefab.GetComponent<Image>().sprite = Corner;
            else if (vector.x == height - 1 && vector.y == width - 1)
                prefab.GetComponent<Image>().sprite = Corner;
            else
                prefab.GetComponent<Image>().sprite = Sprite;

            if(vector.x==0)
                prefab.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (vector.x == height-1)
                prefab.transform.rotation = Quaternion.Euler(0, 0, 180);
            if (vector.y==0 && vector.x!=0)
                prefab.transform.rotation = Quaternion.Euler(0, 0, 90);
            if (vector.y == width-1 && vector.x != height-1)
                prefab.transform.rotation = Quaternion.Euler(0, 0, -90);

            return prefab;
        }

        public override Tile GetTile(Vector2 vector, int width, int height)
        {
            if (vector.x == 0 && vector.y == 0)
                return TilesCorner[0];
            else if (vector.x == 0 && vector.y == width - 1)
                return TilesCorner[1];
            else if (vector.x == height - 1 && vector.y == 0)
                return TilesCorner[3];
            else if (vector.x == height - 1 && vector.y == width - 1)
                return TilesCorner[2];


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
