using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Project.Runtime._Scripts.Gameplay
{
    public class TileSpawner : MonoBehaviour
    {
        // Number of straight tiles from the origin tile to give the player leeway
        [SerializeField] private int tileStartCount = 10;
        [SerializeField] private int minStraightTiles = 3;
        [SerializeField] private int maxStraightTiles = 15;
        
        [SerializeField] private GameObject originTile;
        [SerializeField] private List<GameObject> turnTiles;
        [SerializeField] private List<GameObject> obstacles;
        
        private Vector3 currentTilePosition = Vector3.zero;
        private Vector3 currentTileDirection = Vector3.forward;
        private GameObject previousTile;

        private List<GameObject> currentTiles;
        private List<GameObject> currentObstacles;

        private void Start() {
            // Initialize Lists
            currentTiles = new List<GameObject>();
            currentObstacles = new List<GameObject>();
            
            // Set the random seed to that of the current date & time in milliseconds
            Random.InitState(System.DateTime.Now.Millisecond);

            // Spawn in the starting tiles
            for (int i = 0; i < tileStartCount; i++) {
                SpawnTile(originTile.GetComponent<Tile>());
            }
            
            SpawnTile(ReturnRandGameObjectFromList(turnTiles).GetComponent<Tile>());
        }

        private void SpawnTile(Tile tile, bool spawnObstacle = false) {
            // Instantiate a new tile
            previousTile = GameObject.Instantiate(tile.gameObject, currentTilePosition, Quaternion.identity);
            
            // Store this tile as an active tile in the scene
            currentTiles.Add(previousTile);
            
            // Position this tile in the scene correctly
            Vector3 tileSize = previousTile.GetComponent<Tile>().roadRenderer.bounds.size;
            currentTilePosition += Vector3.Scale(tileSize, currentTileDirection);
        }

        private GameObject ReturnRandGameObjectFromList(List<GameObject> list) {
            if (list.Count <= 0) return null;

            return list[Random.Range(0, list.Count)];
        }
    }
}
