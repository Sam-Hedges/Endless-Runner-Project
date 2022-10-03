using System;
using UnityEngine;
using Cinemachine;

namespace Project.Runtime._Scripts.Gameplay
{
    [Flags]
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
        public Transform startPivot;
        public Transform endPivot;
        public MeshRenderer roadRenderer;
        public CinemachinePath[] lanes = new CinemachinePath[0];
    }
}
