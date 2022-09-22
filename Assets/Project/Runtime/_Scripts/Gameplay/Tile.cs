using UnityEngine;

namespace Project.Runtime._Scripts.Gameplay
{
    public enum TileType
    {
        STRAIGHT,
        LEFT,
        RIGHT,
        BI,
        QUAD
    }
    
    public class Tile : MonoBehaviour
    {
        public TileType type;
        public Transform pivot;
        public MeshRenderer roadRenderer;
    }
}
