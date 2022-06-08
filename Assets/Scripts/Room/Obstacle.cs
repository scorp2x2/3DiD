using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GenerateLvl
{
    [CreateAssetMenu(menuName = "RoomElement/Obstacle")]
    public class Obstacle : RoomElement
    {
        public override string symbol { get { return "o "; } }

        public GameObject prefab;

        public List<Sprite> Sprites;
        public List<Tile> Tiles;

        public override GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            if (Sprites.Count > 0)
                prefab.GetComponent<Image>().sprite = Sprites[Random.Range(0, Sprites.Count)];
            prefab.transform.rotation = Quaternion.Euler(0, 0, 0);

            return prefab;
        }

        public override Tile GetTile(Vector2 vector, int width, int height)
        {
            if (Tiles.Count > 0)
                return Tiles.GetRandom();

            return null;
        }
    }
}
