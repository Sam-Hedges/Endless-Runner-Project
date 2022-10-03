using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;


namespace Project.Runtime._Scripts.Gameplay
{
    public class TileSpawner : MonoBehaviour
    {
        private ObjectPool<Tile> _pool;

        // Number of straight tiles from the origin tile to give the player leeway
        [SerializeField] private int tileStartCount = 10;
        [SerializeField] private int minStraightTiles = 3;
        [SerializeField] private int maxStraightTiles = 15;
        
        [SerializeField] private GameObject startTile;
        
        [SerializeField] private GameObject straightTile;
        [SerializeField] private List<GameObject> turnTiles;
        [SerializeField] private List<GameObject> obstacles;
        
        private Vector3 currentTilePosition = Vector3.zero;
        private Vector3 currentTileDirection = Vector3.forward;
        private GameObject previousTile;

        private List<GameObject> currentTiles;
        private List<GameObject> currentObstacles;

        private void Start() {
            // Initialize Variables
            //_pool = new ObjectPool<Tile>(() => { });
            currentTiles = new List<GameObject>();
            currentObstacles = new List<GameObject>();
            
            // Set the random seed to that of the current date & time in milliseconds
            Random.InitState(System.DateTime.Now.Millisecond);

            previousTile = startTile;
            currentTiles.Add(previousTile);
            
            // Spawn in the starting tiles
            for (int i = 0; i < tileStartCount; i++) {
                SpawnTile(straightTile.GetComponent<Tile>());
            }
            
            //SpawnTile(ReturnRandGameObjectFromList(turnTiles).GetComponent<Tile>());
            SpawnTile(turnTiles[0].GetComponent<Tile>());
            AddNewDirection(Vector3.left);
        }

        private void SpawnTile(Tile tile, bool spawnObstacle = false) {

            // Store the previous' tiles end postion before it's changed by creating the new tile
            Vector3 previousEndPivotPosition = previousTile.GetComponent<Tile>().endPivot.position;

            // Instantiate the new tile
            previousTile = GameObject.Instantiate(tile.gameObject, Vector3.zero, tile.transform.rotation);

            // Rotate this tile in the scene correctly
            previousTile.transform.rotation = previousTile.gameObject.transform.rotation *
                                         Quaternion.LookRotation(currentTileDirection, Vector3.up);

            // Position this tile in the scene correctly
            previousTile.transform.position = previousEndPivotPosition - (previousTile.GetComponent<Tile>().startPivot.position - previousTile.transform.position);

            // Store this tile as an active tile in the scene
            currentTiles.Add(previousTile);
        }

        public void AddNewDirection(Vector3 direction) {
            currentTileDirection = direction;

            Vector3 tilePlacementScale = Vector3.zero;
            switch (previousTile.GetComponent<Tile>().type) {
                case TileType.LEFT:
                case TileType.RIGHT:
                    tilePlacementScale = Vector3.Scale((previousTile.GetComponent<Tile>().roadRenderer.bounds.size - (Vector3.one * 2)) + 
                                                       (Vector3.one * straightTile.GetComponentInChildren<BoxCollider>().size.z / 2), currentTileDirection);
                    break;
                case TileType.QUAD:
                    break;
                case TileType.BI:
                    tilePlacementScale = Vector3.Scale(previousTile.GetComponent<Tile>().roadRenderer.bounds.size / 2 +
                                                       (Vector3.one * straightTile.GetComponentInChildren<BoxCollider>()
                                                           .size.z / 2), currentTileDirection);
                    break;
            }
            
            currentTilePosition += tilePlacementScale;

            int currentPathLength = Random.Range(minStraightTiles, maxStraightTiles);
            for (int i = 0; i < currentPathLength; i++) {
                        
                SpawnTile(straightTile.GetComponent<Tile>(), (i != 0));
            }
                    
            SpawnTile(ReturnRandGameObjectFromList(turnTiles).GetComponent<Tile>());
        }
        
        private Vector3 RotatePointAroundPivot(Vector3 childPosition, Vector3 parentPosition, Vector3 rotation) {
            return Quaternion.Euler(rotation) * (childPosition - parentPosition) + parentPosition;
        }
        
        private GameObject ReturnRandGameObjectFromList(List<GameObject> list) {
            if (list.Count <= 0) return null;

            return list[Random.Range(0, list.Count)];
        }

        public List<GameObject> GetTiles()
        {
            return currentTiles;
        }
    }
}
