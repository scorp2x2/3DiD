using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GenerateLvl
{
    [CreateAssetMenu(menuName = "RoomElement/Floor")]
    public class Floor : RoomElement
    {
        public override string symbol { get { return "  "; } }

        public List<Sprite> Sprites;

        public List<Tile> Tiles;

        public override GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            if (Sprites.Count > 0)
                @object.GetComponent<Image>().sprite = Sprites.GetRandom();
            @object.transform.rotation = Quaternion.Euler(0, 0, 0);

            return @object;
        }

        public override Tile GetTile(Vector2 vector, int width, int height)
        {
            if (Tiles.Count > 0)
                return Tiles.GetRandom();

            return null;
        }
    }
}
