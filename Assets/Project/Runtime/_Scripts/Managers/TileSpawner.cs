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
            
            //SpawnTile(ReturnRandGameObjectFromList(turnTiles).GetComponent<Tile>());
            SpawnTile(turnTiles[0].GetComponent<Tile>());
            AddNewDirection(Vector3.left);
        }

        private void SpawnTile(Tile tile, bool spawnObstacle = false) {

            Quaternion newTileRotation = tile.gameObject.transform.rotation *
                                         Quaternion.LookRotation(currentTileDirection, Vector3.up);
            
            // Instantiate a new tile
            previousTile = GameObject.Instantiate(tile.gameObject, currentTilePosition + tile.transform.localPosition, newTileRotation);
            
            // Store this tile as an active tile in the scene
            currentTiles.Add(previousTile);
            
            // Position this tile in the scene correctly
            if (tile.type == TileType.STRAIGHT) {
                Vector3 tileSize = previousTile.GetComponent<Tile>().roadRenderer.bounds.size;
                currentTilePosition += Vector3.Scale(tileSize, currentTileDirection);
            }
        }

        public void AddNewDirection(Vector3 direction) {
            currentTileDirection = direction;

            Vector3 tilePlacementScale = Vector3.zero;
            switch (previousTile.GetComponent<Tile>().type) {
                case TileType.LEFT:
                case TileType.RIGHT:
                    tilePlacementScale = Vector3.Scale((previousTile.GetComponent<Tile>().roadRenderer.bounds.size - (Vector3.one * 2)) + 
                                                       (Vector3.one * originTile.GetComponentInChildren<BoxCollider>().size.z / 2), currentTileDirection);
                    break;
                case TileType.QUAD:
                    break;
                case TileType.BI:
                    tilePlacementScale = Vector3.Scale(previousTile.GetComponent<Tile>().roadRenderer.bounds.size / 2 +
                                                       (Vector3.one * originTile.GetComponentInChildren<BoxCollider>()
                                                           .size.z / 2), currentTileDirection);
                    break;
            }
            
            currentTilePosition += tilePlacementScale;

            int currentPathLength = Random.Range(minStraightTiles, maxStraightTiles);
            for (int i = 0; i < currentPathLength; i++) {
                        
                SpawnTile(originTile.GetComponent<Tile>(), (i != 0));
            }
                    
            SpawnTile(ReturnRandGameObjectFromList(turnTiles).GetComponent<Tile>());
        }
        
        private GameObject ReturnRandGameObjectFromList(List<GameObject> list) {
            if (list.Count <= 0) return null;

            return list[Random.Range(0, list.Count)];
        }
    }
}
