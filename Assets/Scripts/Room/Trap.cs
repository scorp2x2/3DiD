using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GenerateLvl
{
    [CreateAssetMenu(menuName = "RoomElement/Trap")]
    public class Trap : RoomElement
    {
        public List<GameObject> prefabs;
        public Tile Tile;
        public override GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            return prefabs.GetRandom();
        }

        public override Tile GetTile(Vector2 vector, int width, int height)
        {
            return Tile;
        }
    }
}
