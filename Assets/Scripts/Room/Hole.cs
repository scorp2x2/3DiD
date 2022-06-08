using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GenerateLvl
{
    [CreateAssetMenu(menuName = "RoomElement/Hole")]
    public class Hole : RoomElement
    {
        public override string symbol { get { return "x "; } }

        public GameObject prefab;
        public Tile Tile;

        public override GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            prefab.GetComponent<Image>().sprite = Sprite;
            prefab.transform.rotation = Quaternion.Euler(0, 0, 0);

            return prefab;
        }

        public override Tile GetTile(Vector2 vector, int width, int height)
        {
            return Tile;
        }
    }
}
