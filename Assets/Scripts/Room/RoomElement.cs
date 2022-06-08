using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace GenerateLvl
{
    public class RoomElement : ScriptableObject
    {
        public virtual string symbol { get; set; }
        public Sprite Sprite;

        public virtual GameObject GetSprite(GameObject @object, Vector2 vector, int width, int height)
        {
            @object.GetComponent<Image>().sprite = Sprite;
            @object.transform.rotation = Quaternion.Euler(0, 0, 0);

            return @object;
        }

        public virtual Tile GetTile(Vector2 vector, int width, int height)
        {
            return null;
        }
    }
}
